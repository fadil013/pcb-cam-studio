using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PcbCamStudio.Communication
{
    public class JobStreamer
    {
        private readonly ISender _sender;
        private CancellationTokenSource? _cts;

        public int CurrentLine { get; private set; }
        public int TotalLines { get; private set; }
        public bool IsPaused { get; private set; }

        public event Action<int, int>? ProgressChanged;   // currentLine, totalLines
        public event Action<string>?   LineExecuted;
        public event Action?           JobCompleted;
        public event Action<string>?   JobFailed;

        public JobStreamer(ISender sender) => _sender = sender;

        public async Task RunAsync(List<string> lines, CancellationToken token = default)
        {
            TotalLines = lines.Count;
            CurrentLine = 0;
            _cts = CancellationTokenSource.CreateLinkedTokenSource(token);

            try
            {
                foreach (var line in lines)
                {
                    _cts.Token.ThrowIfCancellationRequested();

                    // Wait while paused
                    while (IsPaused && !_cts.Token.IsCancellationRequested)
                        await Task.Delay(100, _cts.Token);

                    var trimmed = line.Trim();
                    if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith(';'))
                    {
                        CurrentLine++;
                        continue;
                    }

                    // Send line, wait for "ok" response from Marlin
                    await _sender.SendLineAsync(trimmed);
                    CurrentLine++;

                    ProgressChanged?.Invoke(CurrentLine, TotalLines);
                    LineExecuted?.Invoke(trimmed);
                }

                JobCompleted?.Invoke();
            }
            catch (OperationCanceledException)
            {
                // Emergency stop — send M112 immediately
                await _sender.SendLineAsync("M112");
                JobFailed?.Invoke($"Job stopped at line {CurrentLine}");
            }
            catch (Exception ex)
            {
                JobFailed?.Invoke(ex.Message);
            }
        }

        public void Pause()  => IsPaused = true;
        public void Resume() => IsPaused = false;
        public void Stop()   => _cts?.Cancel();
    }
}
