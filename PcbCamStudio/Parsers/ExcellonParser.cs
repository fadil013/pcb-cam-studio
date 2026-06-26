using System;
using System.Collections.Generic;
using System.IO;
using PcbCamStudio.Models;

namespace PcbCamStudio.Parsers
{
    public class ExcellonParser
    {
        public Layer Parse(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Drill file not found: {filePath}");

            var layer = new Layer { FilePath = filePath, Type = LayerType.Drill };
            var lines = File.ReadAllLines(filePath);

            // TODO: implement Excellon parsing
            // Phase 1: header — tool definitions (T1C0.8, T2C1.0...)
            // Phase 2: body   — tool select (T1) + hole coords (X...Y...)
            // Phase 3: group holes by tool code

            return layer;
        }
    }
}
