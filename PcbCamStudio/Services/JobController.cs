using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PcbCamStudio.Cam;
using PcbCamStudio.Communication;
using PcbCamStudio.GCode;
using PcbCamStudio.Models;

namespace PcbCamStudio.Services
{
    public class JobController
    {
        private readonly JobStreamer _streamer;
        private readonly IGCodeWriter _writer;

        public event Action<string>? StatusChanged;
        public event Action<int, int>? ProgressChanged;
        public event Action? JobCompleted;
        public event Action<string>? JobFailed;

        public JobController(ISender sender)
        {
            _streamer = new JobStreamer(sender);
            _writer   = new MarlinWriter();

            _streamer.ProgressChanged += (cur, tot) => ProgressChanged?.Invoke(cur, tot);
            _streamer.JobCompleted    += () => JobCompleted?.Invoke();
            _streamer.JobFailed       += msg => JobFailed?.Invoke(msg);
        }

        // Full one-click job: engrave → drill → cutout
        public async Task RunFullJobAsync(PcbProject project)
        {
            var allLines = new List<string>();

            allLines.AddRange(_writer.WriteHeader(project.Machine));

            foreach (var tp in project.Toolpaths)
            {
                StatusChanged?.Invoke($"Running: {tp.Label}");
                var lines = tp.Type switch
                {
                    ToolpathType.IsolationRoute => _writer.WriteIsolation(tp, project.Machine),
                    ToolpathType.Drill          => _writer.WriteDrilling(tp, project.Machine),
                    ToolpathType.BoardCutout    => _writer.WriteCutout(tp, project.Machine),
                    _                           => new List<string>()
                };
                allLines.AddRange(lines);
            }

            allLines.AddRange(_writer.WriteFooter(project.Machine));

            await _streamer.RunAsync(allLines);
        }

        public void Pause()  => _streamer.Pause();
        public void Resume() => _streamer.Resume();
        public void Stop()   => _streamer.Stop();
    }
}
