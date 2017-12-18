using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimBase
{
    public class CompositeStreamable<T> : IStreamable<T>
    {
        private DoubleRangeMap<IStreamable<T>> m_Streamables;

        public CompositeStreamable()
        {
            m_Streamables = new DoubleRangeMap<IStreamable<T>>(null);
        }

        public void Add(IStreamMoment start, IStreamMoment end, IStreamable<T> streamable)
        {
            m_Streamables.Add(start.PositionInStream, end.PositionInStream, streamable);
        }

        public void Add(IStreamMoment start, IStreamable<T> streamable)
        {
            m_Streamables.Add(start.PositionInStream, streamable);
        }

        public T AtMoment(IStreamMoment moment)
        {
            var streamable = m_Streamables[moment.PositionInStream];
            if (streamable == null) throw new ArgumentException("No streamable has been given for that stream moment");
            return streamable.AtMoment(moment);
        }
    }
}
