using System;
using System.Collections.Generic;
using System.Windows;

namespace PcbCamStudio.Geometry
{
    public static class GeometryUtils
    {
        public static double PolygonArea(List<Point> pts)
        {
            double area = 0;
            int n = pts.Count;
            for (int i = 0; i < n; i++)
            {
                var a = pts[i];
                var b = pts[(i + 1) % n];
                area += a.X * b.Y - b.X * a.Y;
            }
            return Math.Abs(area) / 2.0;
        }

        public static (double minX, double minY, double maxX, double maxY) BoundingBox(List<List<Point>> polygons)
        {
            double minX = double.MaxValue, minY = double.MaxValue;
            double maxX = double.MinValue, maxY = double.MinValue;

            foreach (var poly in polygons)
                foreach (var p in poly)
                {
                    if (p.X < minX) minX = p.X;
                    if (p.Y < minY) minY = p.Y;
                    if (p.X > maxX) maxX = p.X;
                    if (p.Y > maxY) maxY = p.Y;
                }

            return (minX, minY, maxX, maxY);
        }

        public static double Distance(Point a, Point b)
        {
            double dx = a.X - b.X, dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
