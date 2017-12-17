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
        private LinkList<Tuple<double, IStreamable<T>>> m_Streamables;

        public CompositeStreamable()
        {
            m_Streamables = new LinkList<Tuple<double, IStreamable<T>>>(new Tuple<double, IStreamable<T>>(0, null));
        }

        public void Add(IStreamMoment start, IStreamMoment end, IStreamable<T> streamable)
        {
            if(start.PositionInStream >= end.PositionInStream)
            {
                throw new ArgumentOutOfRangeException(nameof(end), end, "Must be greater than start position");
            }
            if(start.PositionInStream < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "Must be greater than or equal to zero");
            }
            if(end.PositionInStream <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(end), end, "Must be greater than zero");
            }

            var lastStreamableBeforeNew = m_Streamables;
            while(lastStreamableBeforeNew.Next != null && lastStreamableBeforeNew.Next.Value.Item1 < start.PositionInStream)
            {
                lastStreamableBeforeNew = lastStreamableBeforeNew.Next;
            }

            var firstStreamableAfterNew = lastStreamableBeforeNew;
            while (firstStreamableAfterNew.Next != null && firstStreamableAfterNew.Next.Value.Item1 < end.PositionInStream)
            {
                firstStreamableAfterNew = firstStreamableAfterNew.Next;
            }

            var newNode = new LinkList<Tuple<double, IStreamable<T>>>(new Tuple<double, IStreamable<T>>(start.PositionInStream, streamable));

            newNode.Next = new LinkList<Tuple<double, IStreamable<T>>>(new Tuple<double, IStreamable<T>>(end.PositionInStream, firstStreamableAfterNew.Value.Item2));
            newNode.Next.Next = firstStreamableAfterNew.Next;

            lastStreamableBeforeNew.Next = newNode;
        }

        public void Add(IStreamMoment start, IStreamable<T> streamable)
        {
            if(start.PositionInStream < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "Must be greater than or equal to zero");
            }

            var lastStreamableBeforeNew = m_Streamables;
            while(lastStreamableBeforeNew.Next != null && lastStreamableBeforeNew.Next.Value.Item1 < start.PositionInStream)
            {
                lastStreamableBeforeNew = lastStreamableBeforeNew.Next;
            }

            var newNode = new LinkList<Tuple<double, IStreamable<T>>>(new Tuple<double, IStreamable<T>>(start.PositionInStream, streamable));

            lastStreamableBeforeNew.Next = newNode;
        }

        public T AtMoment(IStreamMoment moment)
        {
            var presentMoment = moment.PositionInStream;
            IStreamable<T> streamable = m_Streamables.Where(t => t.Item1 <= presentMoment).LastOrDefault()?.Item2;
            if (streamable == null) throw new ArgumentException("No streamable has been given for that stream moment");
            return streamable.AtMoment(moment);
        }

        private class LinkList<TList> : IEnumerable<TList>
        {
            public TList Value;
            public LinkList<TList> Next = null;

            public IEnumerator<TList> GetEnumerator()
            {
                return GetValues().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private IEnumerable<TList> GetValues()
            {
                return new TList[] { Value }.Concat(Next?.GetValues() ?? Enumerable.Empty<TList>());
            }     
            
            public LinkList(TList head)
            {
                Value = head;
            }       
        }
    }
}
