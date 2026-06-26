using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PcbCamStudio.Communication
{
    public class WiFiSender : ISender, IDisposable
    {
        private TcpClient? _client;
        private NetworkStream? _stream;
        private readonly string _ipAddress;
        private readonly int _port;

        public bool IsConnected => _client?.Connected ?? false;

        public WiFiSender(string ipAddress, int port = 23)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        public async Task<bool> ConnectAsync()
        {
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(_ipAddress, _port);
                _stream = _client.GetStream();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Disconnect()
        {
            _stream?.Close();
            _client?.Close();
        }

        public async Task SendLineAsync(string line)
        {
            if (_stream == null) return;
            var data = Encoding.ASCII.GetBytes(line + "\n");
            await _stream.WriteAsync(data);
        }

        public async Task<string> SendAndWaitAsync(string line)
        {
            await SendLineAsync(line);
            var buf = new byte[256];
            int n = await _stream!.ReadAsync(buf);
            return Encoding.ASCII.GetString(buf, 0, n).Trim();
        }

        public void Dispose()
        {
            _stream?.Dispose();
            _client?.Dispose();
        }
    }
}
