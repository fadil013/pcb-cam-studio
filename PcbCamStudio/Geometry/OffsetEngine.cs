using System.Collections.Generic;
using System.Windows;
using Clipper2Lib;

namespace PcbCamStudio.Geometry
{
    public static class OffsetEngine
    {
        private const double Scale = 1_000_000.0; // mm → Clipper integer units

        // Offsets a polygon outward by 'delta' mm (positive = expand)
        public static List<List<Point>> Offset(List<Point> polygon, double delta)
        {
            var path = ToClipperPath(polygon);
            var co = new ClipperOffset();
            co.AddPath(path, JoinType.Round, EndType.Polygon);

            var solution = new Paths64();
            co.Execute(delta * Scale, solution);

            return FromClipperPaths(solution);
        }

        // Offsets multiple polygons at once (union result)
        public static List<List<Point>> OffsetAll(List<List<Point>> polygons, double delta)
        {
            var co = new ClipperOffset();
            foreach (var poly in polygons)
                co.AddPath(ToClipperPath(poly), JoinType.Round, EndType.Polygon);

            var solution = new Paths64();
            co.Execute(delta * Scale, solution);

            return FromClipperPaths(solution);
        }

        private static Path64 ToClipperPath(List<Point> pts)
        {
            var path = new Path64(pts.Count);
            foreach (var p in pts)
                path.Add(new Point64((long)(p.X * Scale), (long)(p.Y * Scale)));
            return path;
        }

        private static List<List<Point>> FromClipperPaths(Paths64 paths)
        {
            var result = new List<List<Point>>(paths.Count);
            foreach (var path in paths)
            {
                var poly = new List<Point>(path.Count);
                foreach (var p in path)
                    poly.Add(new Point(p.X / Scale, p.Y / Scale));
                result.Add(poly);
            }
            return result;
        }
    }
}
