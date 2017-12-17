using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimBase
{
    public class TimeSpanMoment : IStreamMoment
    {
        private TimeSpan m_TimeSpan;
        public TimeSpanMoment(TimeSpan timeSpan)
        {
            m_TimeSpan = timeSpan;
        }

    }
}
