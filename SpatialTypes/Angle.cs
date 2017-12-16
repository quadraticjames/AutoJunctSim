using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialTypes
{
    public class Angle
    {
        public double Radians { get; private set; }
        public double Degrees => Radians * 180.0 / Math.PI;

        private Angle(double r)
        {
            Radians = r;
        }

        public static Angle FromRadians(double r)
        {
            return new Angle(r);
        }
        public static Angle FromDegrees(double d)
        {
            var r = d * Math.PI / 180.0;
            return new Angle(r);
        }
    }
}
