using System;
using SimBase;
using SpatialTypes;

namespace AutoJunctSim
{
    public class StreamableVehicleSprite : IStreamable<IVehicleSprite>
    {
        private SamplingStreamable<Point> CentrePoint;
        private Size Size;
        private SamplingStreamable<Angle> Heading;
        private Guid Guid;

        private IVehicleSprite m_Sprite;

        public StreamableVehicleSprite(IVehicleSprite sprite)
        {
            CentrePoint = new SamplingStreamable<Point>();
            Heading = new SamplingStreamable<Angle>();
            Size = sprite.Size;
            Guid = sprite.Guid;

            m_Sprite = sprite;
        }

        public void TakeSample(IStreamMoment moment)
        {
            CentrePoint.AddSample(m_Sprite.CentrePoint, moment);
            Heading.AddSample(m_Sprite.Heading, moment);
        }

        public IVehicleSprite AtMoment(IStreamMoment moment)
        {
            return new ConcreteVehicleSprite(
                CentrePoint.AtMoment(moment),
                Size,
                Heading.AtMoment(moment),
                Guid);
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
                Size = size;
                Heading = heading;
                Guid = guid;
            }
        }
    }
}
