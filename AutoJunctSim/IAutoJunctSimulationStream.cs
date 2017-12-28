using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimBase;

namespace AutoJunctSim
{
    public interface IAutoJunctSimulationStream
    {
        IStreamable<IList<IStreamable<IVehicleSprite>>> Vehicles { get; }
    }
}
