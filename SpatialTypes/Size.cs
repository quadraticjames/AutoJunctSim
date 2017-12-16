using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialTypes
{
    public class Size
    {
        private Point m_Point;
        public double X => m_Point.X;
        public double Y => m_Point.Y;
        public Size(double x, double y)
        {
            m_Point = new Point(x, y);
        }
        private Size(Point p)
        {
            m_Point = p;
        }
        public static Size operator*(Size size, double scalar)
        {
            return new Size(size.m_Point * scalar);
        }
    }
}
