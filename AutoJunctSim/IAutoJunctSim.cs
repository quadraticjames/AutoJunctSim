﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimBase;

namespace AutoJunctSim
{
    public interface IAutoJunctSimulation : ISimulation
    {
        IList<IVehicleSprite> Vehicles { get; }
    }
}
