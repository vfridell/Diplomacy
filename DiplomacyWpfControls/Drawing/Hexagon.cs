using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DiplomacyWpfControls.Drawing
{
    public static class Hexagon 
    {
        public static Polygon Get()
        {
            return Get(1, new Point(0, 0));
        }

        public static Polygon Get(double size, Point center)
        {
            var polygon = new Polygon();
            for (int i = 1; i <= 6; i++)
            {
                polygon.Points.Add(HexCorner(center, size, i));
            }
            return polygon;
        }

        private static Point HexCorner(Point center, double size, int corner_number)
        {
            double angle_deg = 60 * corner_number + 30;
            double angle_rad = Math.PI / 180 * angle_deg;
            return new Point(center.X + size * Math.Cos(angle_rad), center.Y + size * Math.Sin(angle_rad));
        }
    }
}
