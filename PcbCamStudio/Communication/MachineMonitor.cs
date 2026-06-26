using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PcbCamStudio.Communication
{
    public class MachineMonitor
    {
        private readonly ISender _sender;
        private CancellationTokenSource? _cts;

        public Point3D Position { get; private set; }
        public event Action<Point3D>? PositionUpdated;

        public MachineMonitor(ISender sender) => _sender = sender;

        public void Start(int intervalMs = 500)
        {
            _cts = new CancellationTokenSource();
            _ = PollAsync(intervalMs, _cts.Token);
        }

        public void Stop() => _cts?.Cancel();

        private async Task PollAsync(int intervalMs, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var response = await _sender.SendAndWaitAsync("M114");
                    Position = ParseM114(response);
                    PositionUpdated?.Invoke(Position);
                }
                catch { /* ignore poll errors */ }

                await Task.Delay(intervalMs, token).ContinueWith(_ => { });
            }
        }

        // Parses "X:10.00 Y:20.00 Z:5.00 E:0.00 Count X:800 Y:1600 Z:2000"
        private static Point3D ParseM114(string response)
        {
            var pos = new Point3D();
            foreach (var token in response.Split(' '))
            {
                if (token.StartsWith("X:") && double.TryParse(token[2..], out double x)) pos.X = x;
                if (token.StartsWith("Y:") && double.TryParse(token[2..], out double y)) pos.Y = y;
                if (token.StartsWith("Z:") && double.TryParse(token[2..], out double z)) pos.Z = z;
            }
            return pos;
        }
    }

    public struct Point3D
    {
        public double X, Y, Z;
        public override string ToString() => $"X:{X:F3}  Y:{Y:F3}  Z:{Z:F3}";
    }
}
