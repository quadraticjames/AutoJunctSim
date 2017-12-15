using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimBase
{
    public interface ISimulation
    {
        void Advance(TimeSpan delta);
    }
}
