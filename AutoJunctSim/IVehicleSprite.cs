using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpatialTypes;

namespace AutoJunctSim
{
    public interface IVehicleSprite
    {
        Point CentrePoint { get; }
        Size Size { get; }
        Angle Heading { get; }
    }
}
