using System.ComponentModel;
using System.Runtime.CompilerServices;
using PcbCamStudio.Communication;
using PcbCamStudio.Models;

namespace PcbCamStudio.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private PcbProject _project = new();
        private string _statusText = "Ready";
        private string _connectionStatus = "Not Connected";
        private bool _isConnected;
        private double _posX, _posY, _posZ;
        private int _progress;
        private string _timeLeft = "--:--";
        private string _consoleLog = string.Empty;

        public PcbProject Project
        {
            get => _project;
            set { _project = value; OnPropertyChanged(); }
        }

        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(); }
        }

        public string ConnectionStatus
        {
            get => _connectionStatus;
            set { _connectionStatus = value; OnPropertyChanged(); }
        }

        public bool IsConnected
        {
            get => _isConnected;
            set { _isConnected = value; OnPropertyChanged(); }
        }

        public double PosX
        {
            get => _posX;
            set { _posX = value; OnPropertyChanged(); }
        }

        public double PosY
        {
            get => _posY;
            set { _posY = value; OnPropertyChanged(); }
        }

        public double PosZ
        {
            get => _posZ;
            set { _posZ = value; OnPropertyChanged(); }
        }

        public int Progress
        {
            get => _progress;
            set { _progress = value; OnPropertyChanged(); }
        }

        public string TimeLeft
        {
            get => _timeLeft;
            set { _timeLeft = value; OnPropertyChanged(); }
        }

        public string ConsoleLog
        {
            get => _consoleLog;
            set { _consoleLog = value; OnPropertyChanged(); }
        }

        public void AppendConsole(string line) =>
            ConsoleLog += $"\n> {line}";

        public void UpdatePosition(Point3D pos)
        {
            PosX = pos.X;
            PosY = pos.Y;
            PosZ = pos.Z;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
