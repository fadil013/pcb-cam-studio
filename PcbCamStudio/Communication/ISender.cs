using System.Threading.Tasks;

namespace PcbCamStudio.Communication
{
    public interface ISender
    {
        bool IsConnected { get; }
        Task<bool> ConnectAsync();
        void Disconnect();
        Task SendLineAsync(string line);
        Task<string> SendAndWaitAsync(string line);
    }
}
