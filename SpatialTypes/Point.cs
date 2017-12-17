using System;

namespace SpatialTypes
{
    public class Point : IInterpolable<Point>
    {
        public readonly double X;
        public readonly double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point operator*(Point a, double scalar)
        {
            return new Point(a.X * scalar, a.Y * scalar);
        }

        public static Point operator+(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point operator-(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public override string ToString()
        {
            return $"{X.ToString("N1")}, {Y.ToString("N1")}";
        }

        public Point Interpolate(Point end, double fraction)
        {
            if (fraction < 0 || fraction > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(fraction), fraction, "May only be between 0 and 1 inclusive");
            }

            return this + ((end - this) * fraction);
        }
    }
}
