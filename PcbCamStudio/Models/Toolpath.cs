using System.Collections.Generic;
using System.Windows;

namespace PcbCamStudio.Models
{
    public enum ToolpathType
    {
        IsolationRoute,
        Drill,
        BoardCutout
    }

    public class ToolpathMove
    {
        public Point Position { get; set; }
        public double Z { get; set; }
        public bool IsRapid { get; set; }   // G0 vs G1
    }

    public class Toolpath
    {
        public ToolpathType Type { get; set; }
        public string Label { get; set; } = string.Empty;
        public double ToolDiameter { get; set; }
        public List<ToolpathMove> Moves { get; set; } = new();
        public int TotalLines { get; set; }
    }
}
