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

            var lastItemBeforeNew = GetItemAtPosition(start, m_Items);

            var firstItemAfterNew = GetItemAtPosition(end, lastItemBeforeNew);

            var newNode = new LinkList<Tuple<double, T>>(new Tuple<double, T>(start, item));

            newNode.Next = new LinkList<Tuple<double, T>>(new Tuple<double, T>(end, firstItemAfterNew.Value.Item2));
            newNode.Next.Next = firstItemAfterNew.Next;

            lastItemBeforeNew.Next = newNode;
        }

        public void Add(double start, T item)
        {
            var lastItemBeforeNew = GetItemAtPosition(start, m_Items);

            var newNode = new LinkList<Tuple<double, T>>(new Tuple<double, T>(start, item));

            lastItemBeforeNew.Next = newNode;
        }

        public T this[double position]
        {
            get
            {
                return GetItemAtPosition(position, m_Items).Value.Item2;
            }
        }

        private static LinkList<Tuple<double, T>> GetItemAtPosition(double position, LinkList<Tuple<double, T>> items)
        {
            while(items.Next != null && items.Next.Value.Item1 <= position)
            {
                items = items.Next;
            }
            return items;
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
