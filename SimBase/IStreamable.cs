using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimBase
{
    public interface IStreamable<T>
    {
        T AtMoment(IStreamMoment moment);
    }
}
