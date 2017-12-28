using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SimBase;
using SpatialTypes;

namespace AutoJunctSim
{
    public class AutoJunctSimulationStream : IAutoJunctSimulationStream
    {
        public IStreamable<IList<IStreamable<IVehicleSprite>>> Vehicles => VehiclesList;
        private StreamableList<IStreamable<IVehicleSprite>> VehiclesList = new StreamableList<IStreamable<IVehicleSprite>>();

        public void Start()
        {
            var thread = new Thread(RunSimulation);
            thread.Start();
        }

        private void RunSimulation()
        {
        }
    }
}
