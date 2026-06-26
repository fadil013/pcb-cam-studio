using System;
using System.Collections.Generic;
using System.Windows;
using PcbCamStudio.Models;

namespace PcbCamStudio.Geometry
{
    public static class PolygonBuilder
    {
        // Converts an aperture flash at (x,y) into a polygon point list
        public static List<Point> ApertureToPolygon(Aperture aperture, double x, double y)
        {
            return aperture.Shape switch
            {
                ApertureShape.Circle    => CirclePolygon(x, y, aperture.Width / 2, 32),
                ApertureShape.Rectangle => RectPolygon(x, y, aperture.Width, aperture.Height),
                ApertureShape.Obround   => ObroundPolygon(x, y, aperture.Width, aperture.Height),
                _                       => CirclePolygon(x, y, aperture.Width / 2, 32)
            };
        }

        public static List<Point> CirclePolygon(double cx, double cy, double r, int segments)
        {
            var pts = new List<Point>(segments);
            for (int i = 0; i < segments; i++)
            {
                double a = 2 * Math.PI * i / segments;
                pts.Add(new Point(cx + r * Math.Cos(a), cy + r * Math.Sin(a)));
            }
            return pts;
        }

        public static List<Point> RectPolygon(double cx, double cy, double w, double h)
        {
            double hw = w / 2, hh = h / 2;
            return new List<Point>
            {
                new(cx - hw, cy - hh),
                new(cx + hw, cy - hh),
                new(cx + hw, cy + hh),
                new(cx - hw, cy + hh)
            };
        }

        public static List<Point> ObroundPolygon(double cx, double cy, double w, double h)
        {
            // TODO: implement obround (stadium shape) approximation
            return RectPolygon(cx, cy, w, h);
        }
    }
}
