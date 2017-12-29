using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialTypes
{
    public class Angle : IInterpolable<Angle>
    {
        public double Radians { get; private set; }
        public double Degrees => Radians * 180.0 / Math.PI;

        private Angle(double r)
        {
            Radians = r;
        }

        public static Angle FromRadians(double r)
        {
            var modulo = Math.PI * 2;
            while(r >= modulo)
            {
                r -= modulo;
            }
            while (r < 0)
            {
                r += modulo;
            }
            return new Angle(r);
        }
        public static Angle FromDegrees(double d)
        {
            var r = d * Math.PI / 180.0;
            return FromRadians(r);
        }

        public Angle Interpolate(Angle end, double fraction)
        {
            if (fraction < 0 || fraction > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(fraction), fraction, "May only be between 0 and 1 inclusive");
            }

            if(Math.Abs(Math.Abs(end.Radians - this.Radians) - Math.PI) < 0.00001)
            {
                throw new ArgumentOutOfRangeException(nameof(end), end, "Angle between this and end must not be 180 degrees");
            }

            var startR = Radians;
            var endR = end.Radians;

            if(startR < Math.PI && endR > Math.PI && endR - startR > Math.PI)
            {
                startR += 2 * Math.PI;
            }
            else if(startR > Math.PI && endR < Math.PI && startR - endR > Math.PI)
            {
                endR += 2 * Math.PI;
            }

            return FromRadians(startR + ((endR - startR) * fraction));
        }

        public static Point PointOnUnitCircleAtAngle(Angle a)
        {
            return new Point(Math.Sin(a.Radians), 0 - Math.Cos(a.Radians));
        }
    }
}
