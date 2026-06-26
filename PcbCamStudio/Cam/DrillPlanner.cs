using System.Collections.Generic;
using System.Linq;
using PcbCamStudio.Models;

namespace PcbCamStudio.Cam
{
    public class DrillPlanner
    {
        public List<Toolpath> Generate(Layer drillLayer, double safeZ, double feedRate)
        {
            var toolpaths = new List<Toolpath>();

            // Group holes by diameter (each group = one tool change)
            var groups = drillLayer.Holes
                .GroupBy(h => h.Diameter)
                .OrderBy(g => g.Key);

            foreach (var group in groups)
            {
                var holes = PathOptimizer.NearestNeighbor(group.ToList());

                var tp = new Toolpath
                {
                    Type = ToolpathType.Drill,
                    Label = $"Drill Ø{group.Key:F2}mm ({holes.Count} holes)",
                    ToolDiameter = group.Key
                };

                // TODO: convert hole list → ToolpathMoves (safeZ lift, rapid to XY, drill down, retract)

                toolpaths.Add(tp);
            }

            return toolpaths;
        }
    }
}
