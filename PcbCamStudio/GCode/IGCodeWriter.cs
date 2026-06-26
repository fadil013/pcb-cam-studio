using System.Collections.Generic;
using PcbCamStudio.Models;

namespace PcbCamStudio.GCode
{
    public interface IGCodeWriter
    {
        List<string> WriteIsolation(Toolpath toolpath, MachineProfile machine);
        List<string> WriteDrilling(Toolpath toolpath, MachineProfile machine);
        List<string> WriteCutout(Toolpath toolpath, MachineProfile machine);
        List<string> WriteHeader(MachineProfile machine);
        List<string> WriteFooter(MachineProfile machine);
    }
}
