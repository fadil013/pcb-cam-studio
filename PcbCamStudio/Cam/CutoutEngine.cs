using System.Collections.Generic;
using PcbCamStudio.Models;

namespace PcbCamStudio.Cam
{
    public class CutoutEngine
    {
        public Toolpath Generate(Layer outlineLayer, double boardThickness,
                                  double depthPerPass, double toolDiameter,
                                  int tabCount, double safeZ, double feedRate)
        {
            int passCount = (int)System.Math.Ceiling(boardThickness / depthPerPass);

            var tp = new Toolpath
            {
                Type = ToolpathType.BoardCutout,
                Label = $"Board Cutout ({passCount} passes, {tabCount} tabs)",
                ToolDiameter = toolDiameter
            };

            // TODO: implement multi-pass cutout
            // 1. Get outline polygon from outlineLayer
            // 2. Offset inward by toolDiameter/2 (tool center path)
            // 3. Calculate tab positions evenly around outline
            // 4. For each depth pass: cut outline, lift over tabs

            return tp;
        }
    }
}
