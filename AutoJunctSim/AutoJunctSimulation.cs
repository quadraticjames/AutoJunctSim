using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimBase;
using SpatialTypes;

namespace AutoJunctSim
{
    public class AutoJunctSimulation : IAutoJunctSimulation
    {
        private TimeSpan m_Elapsed = TimeSpan.Zero;
        private IAutoJunctSimulationStream m_SimulationStream = new AutoJunctSimulationStream();
        public void Advance(TimeSpan deltaTime)
        {
            m_Elapsed += deltaTime;

            var moment = new TimeSpanMoment(m_Elapsed);

            Vehicles = m_SimulationStream
                .Vehicles
                .AtMoment(new TimeSpanMoment(m_Elapsed))
                .Select(s => s.AtMoment(moment))
                .ToList();
        }

        public IList<IVehicleSprite> Vehicles
        {
            get; private set;
        }
    }

    public interface IAutoJunctSimulationStream
    {
        IStreamable<IList<IStreamable<IVehicleSprite>>> Vehicles { get; }
    }

    public class AutoJunctSimulationStream : IAutoJunctSimulationStream
    {
        public IStreamable<IList<IStreamable<IVehicleSprite>>> Vehicles { get; }

        public AutoJunctSimulationStream()
        {
            var sprite = new StreamableVehicleSprite();

            var vehicles = new StreamableList<IStreamable<IVehicleSprite>>();
            vehicles.AddAt(sprite, new TimeSpanMoment(TimeSpan.Zero));
            vehicles.RemoveAt(sprite, new TimeSpanMoment(TimeSpan.FromSeconds(10)));
            Vehicles = vehicles;
        }
    }

    public class StreamableVehicleSprite : IStreamable<IVehicleSprite>
    {
        private IStreamable<Point> m_CentrePoint;
        private IStreamable<Angle> m_Heading;
        private Size m_Size;
        private Guid m_Guid;

        public StreamableVehicleSprite()
        {
            m_Size = new Size(2.0, 5.0);
            m_Guid = Guid.NewGuid();
            m_CentrePoint = new InterpolatingStreamable<Point>(new Point(0, 0), new Point(134, 134), new TimeSpanMoment(TimeSpan.Zero), new TimeSpanMoment(TimeSpan.FromSeconds(10)));
            m_Heading = new ConstantStreamable<Angle>(Angle.FromDegrees(135));
        }

        public IVehicleSprite AtMoment(IStreamMoment moment)
        {
            return new ConcreteVehicleSprite(m_CentrePoint.AtMoment(moment), m_Size, m_Heading.AtMoment(moment), m_Guid);
        }

        private class ConcreteVehicleSprite : IVehicleSprite
        {
            public Point CentrePoint { get; }
            public Size Size { get; }
            public Angle Heading { get; }
            public Guid Guid { get; }

            public ConcreteVehicleSprite(Point centrePoint, Size size, Angle heading, Guid guid)
            {
                CentrePoint = centrePoint;
                Heading = heading;
                Size = size;
                Guid = guid;
            }
        }
    }
}
