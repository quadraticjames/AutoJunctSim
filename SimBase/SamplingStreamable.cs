using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpatialTypes;

namespace SimBase
{
    public class SamplingStreamable<T> : IStreamable<T> where T : IInterpolable<T>
    {
        private CompositeStreamable<T> m_Streamable;

        public SamplingStreamable()
        {
            m_Streamable = new CompositeStreamable<T>();
        }

        private bool m_FirstSampleSeen = false;
        private T m_LastItem;
        private IStreamMoment m_LastMoment;

        public void AddSample(T item, IStreamMoment moment)
        {
            if(m_FirstSampleSeen)
            {
                if (moment.PositionInStream <= m_LastMoment.PositionInStream)
                {
                    throw new ArgumentOutOfRangeException(nameof(moment), "Samples must be submitted in chronological order");
                }
                m_Streamable.Add(m_LastMoment, moment, new InterpolatingStreamable<T>(m_LastItem, item, m_LastMoment, moment));
            }
            m_LastItem = item;
            m_LastMoment = moment;
            m_FirstSampleSeen = true;
        }

        public T AtMoment(IStreamMoment moment)
        {
            return m_Streamable.AtMoment(moment);
        }
    }
}
