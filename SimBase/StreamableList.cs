using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimBase
{
    public class StreamableList<T> : IStreamable<IList<T>>
    {
        private class Log
        {
            public double Time;
            public T Item;
        }

        private List<Log> m_Log = new List<Log>();

        public IList<T> AtMoment(IStreamMoment moment)
        {
            var now = moment.PositionInStream;
            return m_Log.TakeWhile(l => l.Time <= now).Select(l => l.Item).ToList();
        }

        public void AddAt(T item, IStreamMoment moment)
        {
            m_Log.Add(new Log()
            {
                Time = moment.PositionInStream,
                Item = item
            });
        }
    }
}
