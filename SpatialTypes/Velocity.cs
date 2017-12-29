using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialTypes
{
    public class Velocity
    {
        private readonly double m_MetresPerSecond;

        public Velocity(double metresPerSecond)
        {
            m_MetresPerSecond = metresPerSecond;
        }

        public double MetresPerSecond => m_MetresPerSecond;

        public static Distance DistanceTravelled(Velocity velocity, TimeSpan time)
        {
            return new Distance(velocity.MetresPerSecond * time.TotalSeconds);
        }

        public static Velocity operator+(Velocity a, Velocity b)
        {
            return new Velocity(a.MetresPerSecond + b.MetresPerSecond);
        }
    }

    public class Distance
    {
        public readonly double Metres;
        public Distance(double metres)
        {
            Metres = metres;
        }
        
        public static Distance operator +(Distance a, Distance b)
        {
            return new Distance(a.Metres + b.Metres);
        }
    }

    public class Acceleration
    {
        private readonly double m_MetresPerSecondPerSecond;

        public Acceleration(double metresPerSecondPerSecond)
        {
            m_MetresPerSecondPerSecond = metresPerSecondPerSecond;
        }

        public double MetresPerSecondPerSecond => m_MetresPerSecondPerSecond;

        public static Velocity VelocityGained(Acceleration acceleration, TimeSpan time)
        {
            return new Velocity(acceleration.MetresPerSecondPerSecond * time.TotalSeconds);
        }

        public static Distance DistanceTravelledUnderConstantAcceleration(Velocity initialVelocity, Acceleration acceleration, TimeSpan time)
        {
            return Velocity.DistanceTravelled(initialVelocity, time) + 
                new Distance(0.5 * acceleration.MetresPerSecondPerSecond * Math.Pow(time.TotalSeconds, 2));
        }
    }
}
