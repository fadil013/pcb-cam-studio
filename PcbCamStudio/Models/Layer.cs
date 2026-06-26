using System.Collections.Generic;

namespace PcbCamStudio.Models
{
    public enum LayerType
    {
        Copper,
        BoardOutline,
        Drill,
        Unknown
    }

    public class Layer
    {
        public string FilePath { get; set; } = string.Empty;
        public string FileName => System.IO.Path.GetFileName(FilePath);
        public LayerType Type { get; set; } = LayerType.Unknown;
        public bool IsVisible { get; set; } = true;

        public List<Aperture> Apertures { get; set; } = new();
        public List<Hole> Holes { get; set; } = new();

        // Raw parsed geometry (list of polygon point lists)
        public List<List<System.Windows.Point>> Polygons { get; set; } = new();
    }
}
