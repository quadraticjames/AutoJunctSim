using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimBase;

namespace AutoJunctSim
{
    public class AutoJunctSimulationStream : IAutoJunctSimulationStream
    {
        public IStreamable<IList<IStreamable<IVehicleSprite>>> Vehicles => VehiclesList;
        private StreamableList<IStreamable<IVehicleSprite>> VehiclesList = new StreamableList<IStreamable<IVehicleSprite>>();
    }
}
