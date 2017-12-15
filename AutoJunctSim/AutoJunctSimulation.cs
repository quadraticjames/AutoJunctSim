using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        } = new List<IVehicleSprite>();
    }
}
