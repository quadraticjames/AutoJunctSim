using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SimBase
{
    public class DoubleRangeMap<T>
    {
        private LinkList<Tuple<double, T>> m_Items;
        public DoubleRangeMap(T defaultValue = default(T))
        {
            m_Items = new LinkList<Tuple<double, T>>(new Tuple<double, T>(double.MinValue, defaultValue));
        }

        public void Add(double start, double end, T item)
        {
            if (start >= end)
            {
                throw new ArgumentOutOfRangeException(nameof(end), end, $"Must be greater than {nameof(start)}");
            }

            var lastItemBeforeNew = m_Items;
            while (lastItemBeforeNew.Next != null && lastItemBeforeNew.Next.Value.Item1 < start)
            {
                lastItemBeforeNew = lastItemBeforeNew.Next;
            }

            var firstItemAfterNew = lastItemBeforeNew;
            while (firstItemAfterNew.Next != null && firstItemAfterNew.Next.Value.Item1 < end)
            {
                firstItemAfterNew = firstItemAfterNew.Next;
            }

            var newNode = new LinkList<Tuple<double, T>>(new Tuple<double, T>(start, item));

            newNode.Next = new LinkList<Tuple<double, T>>(new Tuple<double, T>(end, firstItemAfterNew.Value.Item2));
            newNode.Next.Next = firstItemAfterNew.Next;

            lastItemBeforeNew.Next = newNode;
        }

        public void Add(double start, T item)
        {
            var lastItemBeforeNew = m_Items;
            while (lastItemBeforeNew.Next != null && lastItemBeforeNew.Next.Value.Item1 < start)
            {
                lastItemBeforeNew = lastItemBeforeNew.Next;
            }

            var newNode = new LinkList<Tuple<double, T>>(new Tuple<double, T>(start, item));

            lastItemBeforeNew.Next = newNode;
        }

        public T this[double position]
        {
            get
            {
                var pointer = m_Items;
                while (pointer.Next != null && pointer.Next.Value.Item1 <= position)
                {
                    pointer = pointer.Next;
                }
                return pointer.Value.Item2;
            }
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
