using System.IO;
using System.Text.Json;
using PcbCamStudio.Models;

namespace PcbCamStudio.Services
{
    public class ProjectService
    {
        private static readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

        public void Save(PcbProject project, string path)
        {
            var json = JsonSerializer.Serialize(project, _opts);
            File.WriteAllText(path, json);
        }

        public PcbProject Load(string path)
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<PcbProject>(json) ?? new PcbProject();
        }
    }
}
