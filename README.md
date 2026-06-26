# PCB CAM Studio

A Windows desktop application for PCB computer-aided manufacturing (CAM): import Gerber + Excellon files, compute isolation-routing and drilling tool-paths, preview them in 2D/3D, generate G-code, and stream the job straight to a CNC mill over serial or Wi-Fi. Built with **WPF on .NET 8** using an MVVM architecture.

## Pipeline

```
Gerber / Excellon  ─▶  Parse  ─▶  Geometry (offset / isolation)  ─▶  Tool-paths
                                                                        │
                                              2D / 3D viewport  ◀───────┤
                                                                        ▼
                                            G-code (Marlin)  ─▶  Serial / Wi-Fi  ─▶  CNC mill
```

## Modules

| Namespace | Implements |
|---|---|
| `Parsers/` | `GerberParser`, `ExcellonParser`, `LayerDetector` — import copper + drill data |
| `Geometry/` | `OffsetEngine`, `PolygonBuilder`, `GeometryUtils` — isolation offsets & polygon math |
| `Cam/` | `IsolationRouter`, `DrillPlanner`, `CutoutEngine`, `PathOptimizer` — tool-path generation |
| `GCode/` | `GCodeExporter`, `MarlinWriter` (`IGCodeWriter`) — post-processing to machine G-code |
| `Communication/` | `SerialSender`, `WiFiSender` (`ISender`), `JobStreamer`, `MachineMonitor` — CNC link |
| `Services/` | `ProjectService`, `JobController` — project + job orchestration |
| `Models/` | `PcbProject`, `Layer`, `Aperture`, `Hole`, `Toolpath`, `MachineProfile` |
| `UI/Views`, `UI/ViewModels` | WPF views (`Viewport2D`, `Viewport3D`, `ControlPanel`) + MVVM view-models |

## Stack

C# · .NET 8 · WPF · MVVM

## Build & run

```bash
cd PcbCamStudio
dotnet restore
dotnet run
```

Requires the .NET 8 SDK and Windows (WPF).

> Build output (`bin/`, `obj/`) and NuGet packages are gitignored; `dotnet restore` regenerates them.

---

Built by **Muhammad Fadil Falak Awan** — [portfolio](https://fadil-awan.vercel.app) · [github.com/fadil013](https://github.com/fadil013)
