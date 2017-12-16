using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpatialTypes;

namespace AutoJunctSim
{
    public class AutoJunctSimulation : IAutoJunctSimulation
    {
        public void Advance(TimeSpan deltaTime)
        {

        }

        public IList<IVehicleSprite> Vehicles
        {
            get; private set;
        } = new List<IVehicleSprite>() { new DummyVehicleSprite() };
    
        private class DummyVehicleSprite : IVehicleSprite
        {
            public Point CentrePoint { get; } = new Point(0, 0);
            public Size Size { get; } = new Size(2, 5);
            public Angle Heading { get; } = Angle.FromDegrees(135);
        }
    }
}
