using System.Collections.Generic;
using System.Windows;
using PcbCamStudio.Geometry;
using PcbCamStudio.Models;

namespace PcbCamStudio.Cam
{
    public static class PathOptimizer
    {
        // Nearest-neighbor TSP — minimizes total travel distance between holes
        public static List<Hole> NearestNeighbor(List<Hole> holes)
        {
            if (holes.Count <= 1) return holes;

            var remaining = new List<Hole>(holes);
            var ordered = new List<Hole>(holes.Count);

            // Start from hole closest to origin
            var current = remaining[0];
            remaining.RemoveAt(0);
            ordered.Add(current);

            while (remaining.Count > 0)
            {
                double bestDist = double.MaxValue;
                int bestIdx = 0;
                var currentPt = new Point(current.X, current.Y);

                for (int i = 0; i < remaining.Count; i++)
                {
                    double d = GeometryUtils.Distance(currentPt, new Point(remaining[i].X, remaining[i].Y));
                    if (d < bestDist) { bestDist = d; bestIdx = i; }
                }

                current = remaining[bestIdx];
                remaining.RemoveAt(bestIdx);
                ordered.Add(current);
            }

            return ordered;
        }
    }
}
