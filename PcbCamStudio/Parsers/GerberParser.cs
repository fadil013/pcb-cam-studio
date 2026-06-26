using System;
using System.Collections.Generic;
using System.IO;
using PcbCamStudio.Models;

namespace PcbCamStudio.Parsers
{
    public class GerberParser
    {
        public Layer Parse(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Gerber file not found: {filePath}");

            var layer = new Layer { FilePath = filePath };
            var lines = File.ReadAllLines(filePath);

            // TODO: implement full RS-274X parsing
            // Phase 1: aperture definitions (%ADD...)
            // Phase 2: draw commands (D01 draw, D02 move, D03 flash)
            // Phase 3: region commands (G36/G37)

            return layer;
        }
    }
}
