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
}
