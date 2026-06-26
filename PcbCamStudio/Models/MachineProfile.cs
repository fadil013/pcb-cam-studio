namespace PcbCamStudio.Models
{
    public enum ConnectionType { USB, WiFi }

    public class MachineProfile
    {
        public string Name { get; set; } = "My CNC";
        public ConnectionType Connection { get; set; } = ConnectionType.USB;

        // USB settings
        public string SerialPort { get; set; } = "COM3";
        public int BaudRate { get; set; } = 115200;

        // WiFi settings
        public string IpAddress { get; set; } = string.Empty;
        public int TcpPort { get; set; } = 23;

        // Work area limits (mm)
        public double WorkAreaX { get; set; } = 300;
        public double WorkAreaY { get; set; } = 300;
        public double WorkAreaZ { get; set; } = 50;

        // Feed rates (mm/min)
        public double RapidFeed { get; set; } = 3000;
        public double CutFeed { get; set; } = 800;
        public double DrillFeed { get; set; } = 100;
        public int SpindleSpeed { get; set; } = 10000;
    }
}
