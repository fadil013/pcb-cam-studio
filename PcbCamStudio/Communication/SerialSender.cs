using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace PcbCamStudio.Communication
{
    public class SerialSender : ISender, IDisposable
    {
        private SerialPort? _port;
        private readonly string _portName;
        private readonly int _baudRate;

        public bool IsConnected => _port?.IsOpen ?? false;

        public SerialSender(string portName, int baudRate)
        {
            _portName = portName;
            _baudRate = baudRate;
        }

        public Task<bool> ConnectAsync()
        {
            try
            {
                _port = new SerialPort(_portName, _baudRate, Parity.None, 8, StopBits.One)
                {
                    ReadTimeout  = 5000,
                    WriteTimeout = 5000,
                    NewLine      = "\n"
                };
                _port.Open();
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public void Disconnect() => _port?.Close();

        public Task SendLineAsync(string line)
        {
            _port?.WriteLine(line);
            return Task.CompletedTask;
        }

        public async Task<string> SendAndWaitAsync(string line)
        {
            await SendLineAsync(line);
            return await Task.Run(() => _port?.ReadLine() ?? string.Empty);
        }

        public static string[] GetAvailablePorts() => SerialPort.GetPortNames();

        public void Dispose() => _port?.Dispose();
    }
}
