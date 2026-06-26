using System.Collections.Generic;
using System.IO;
using System.Text;
using PcbCamStudio.Models;

namespace PcbCamStudio.GCode
{
    public class GCodeExporter
    {
        private readonly IGCodeWriter _writer;

        public GCodeExporter(IGCodeWriter writer) => _writer = writer;

        public void Export(PcbProject project, string outputPath)
        {
            var sb = new StringBuilder();

            foreach (var line in _writer.WriteHeader(project.Machine))
                sb.AppendLine(line);

            foreach (var tp in project.Toolpaths)
            {
                var lines = tp.Type switch
                {
                    ToolpathType.IsolationRoute => _writer.WriteIsolation(tp, project.Machine),
                    ToolpathType.Drill          => _writer.WriteDrilling(tp, project.Machine),
                    ToolpathType.BoardCutout    => _writer.WriteCutout(tp, project.Machine),
                    _                           => new List<string>()
                };
                foreach (var line in lines)
                    sb.AppendLine(line);
            }

            foreach (var line in _writer.WriteFooter(project.Machine))
                sb.AppendLine(line);

            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }
    }
}
