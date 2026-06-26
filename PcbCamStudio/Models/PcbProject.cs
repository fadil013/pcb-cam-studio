using System.Collections.Generic;

namespace PcbCamStudio.Models
{
    public class PcbProject
    {
        public string Name { get; set; } = "Untitled Project";
        public string FilePath { get; set; } = string.Empty;

        public List<Layer> Layers { get; set; } = new();
        public List<Toolpath> Toolpaths { get; set; } = new();
        public MachineProfile Machine { get; set; } = new();

        // CAM settings
        public double ToolDiameter { get; set; } = 0.1;
        public int IsolationPasses { get; set; } = 2;
        public double CutDepth { get; set; } = -0.1;
        public double SafeZ { get; set; } = 2.0;
        public double DrillFeedRate { get; set; } = 100;
        public double BoardThickness { get; set; } = 1.6;
        public double CutoutDepthPerPass { get; set; } = 0.5;
        public int HoldingTabs { get; set; } = 4;
    }
}
