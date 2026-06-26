namespace PcbCamStudio.Models
{
    public class Hole
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Diameter { get; set; }
        public int ToolCode { get; set; }   // T1, T2, etc. from Excellon
    }
}
