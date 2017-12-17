using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpatialTypes;

namespace SimBase
{
    public class InterpolatingStreamable<T> : IStreamable<T> where T : IInterpolable<T>
    {
        private T m_Start;
        private T m_End;
        private double m_StartMoment;
        private double m_EndMoment;

        public InterpolatingStreamable(T start, T end, IStreamMoment startMoment, IStreamMoment endMoment)
        {
            m_Start = start;
            m_End = end;
            m_StartMoment = startMoment.PositionInStream;
            m_EndMoment = endMoment.PositionInStream;
        }

        public T AtMoment(IStreamMoment streamMoment)
        {
            var moment = streamMoment.PositionInStream;
            if(moment < m_StartMoment || m_EndMoment < moment)
            {
                throw new ArgumentOutOfRangeException(nameof(moment), moment, "Must be within start and end of range");
            }

            return m_Start.Interpolate(m_End, (moment - m_StartMoment) / (m_EndMoment - m_StartMoment));
        }
    }
}
