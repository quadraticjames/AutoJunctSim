using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpatialTypes;

namespace SimRunner
{
    public interface ICanvasWriter
    {
        void DrawRectangle(Point centrePoint, Size size, Angle heading);
    }
}
