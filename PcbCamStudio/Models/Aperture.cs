namespace PcbCamStudio.Models
{
    public enum ApertureShape
    {
        Circle,
        Rectangle,
        Obround,
        Polygon,
        Macro
    }

    public class Aperture
    {
        public int Code { get; set; }               // D10, D11, etc.
        public ApertureShape Shape { get; set; }
        public double Width { get; set; }           // X size / diameter
        public double Height { get; set; }          // Y size (for rect/obround)
        public double HoleDiameter { get; set; }    // inner hole if any
    }
}
