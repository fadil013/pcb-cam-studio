using System.IO;
using PcbCamStudio.Models;

namespace PcbCamStudio.Parsers
{
    public static class LayerDetector
    {
        public static LayerType Detect(string filePath)
        {
            var ext = Path.GetExtension(filePath).ToLowerInvariant();
            var name = Path.GetFileNameWithoutExtension(filePath).ToLowerInvariant();

            // Extension-based detection
            return ext switch
            {
                ".gtl" or ".cmp" or ".top"               => LayerType.Copper,
                ".gbl" or ".sol" or ".bot"               => LayerType.Copper,
                ".gko" or ".gml"                         => LayerType.BoardOutline,
                ".gbr" when name.Contains("outline") || name.Contains("edge") || name.Contains("border")
                                                         => LayerType.BoardOutline,
                ".drl" or ".exc" or ".xln" or ".ncd"    => LayerType.Drill,
                ".gbr"                                   => LayerType.Copper,
                _                                        => LayerType.Unknown
            };
        }
    }
}
