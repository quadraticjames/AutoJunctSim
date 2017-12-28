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
            public LogType Type;

            public enum LogType
            {
                Add,
                Remove
            }
        }

        private List<Log> m_Log = new List<Log>();

        public IList<T> AtMoment(IStreamMoment moment)
        {
            var now = moment.PositionInStream;
            var logs = m_Log.TakeWhile(l => l.Time <= now).ToList();
            var added = new List<Log>();
            var removed = new List<Log>();
            foreach(var log in logs)
            {
                if(log.Type == Log.LogType.Add)
                {
                    added.Add(log);
                }
                else
                {
                    removed.Add(log);
                }
            }
            var removedItems = removed.Select(l => l.Item).ToList();
            return added.Select(l => l.Item).Except(removedItems).ToList();
        }

        public void AddAt(T item, IStreamMoment moment)
        {
            m_Log.Add(new Log()
            {
                Time = moment.PositionInStream,
                Item = item,
                Type = Log.LogType.Add
            });
        }

        public void RemoveAt(T item, IStreamMoment moment)
        {
            m_Log.Add(new Log()
            {
                Time = moment.PositionInStream,
                Item = item,
                Type = Log.LogType.Remove
            });
        }
    }
}
