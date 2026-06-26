using System.Collections.Generic;
using System.Windows;
using PcbCamStudio.Geometry;
using PcbCamStudio.Models;

namespace PcbCamStudio.Cam
{
    public class IsolationRouter
    {
        public List<Toolpath> Generate(Layer copperLayer, double toolDiameter, int passes, double cutDepth, double safeZ)
        {
            var toolpaths = new List<Toolpath>();
            double toolRadius = toolDiameter / 2.0;

            for (int pass = 1; pass <= passes; pass++)
            {
                double offset = toolRadius + (pass - 1) * toolDiameter;
                var offsetPolygons = OffsetEngine.OffsetAll(copperLayer.Polygons, offset);

                var tp = new Toolpath
                {
                    Type = ToolpathType.IsolationRoute,
                    Label = $"Isolation Pass {pass}",
                    ToolDiameter = toolDiameter
                };

                // TODO: convert offsetPolygons → ToolpathMoves (lift Z, rapid to start, cut along path)

                toolpaths.Add(tp);
            }

            return toolpaths;
        }
    }
}
