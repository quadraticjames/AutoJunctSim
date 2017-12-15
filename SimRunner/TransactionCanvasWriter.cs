using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpatialTypes;

namespace SimRunner
{
    public class TransactionCanvasWriter : ICanvasWriter
    {
        public TransactionCanvasWriter(ICanvasWriter baseCanvasWriter)
        {
            m_BaseCanvasWriter = baseCanvasWriter;
        }
        private ICanvasWriter m_BaseCanvasWriter;

        private bool m_Used = false;
        private List<Action<ICanvasWriter>> m_Actions = new List<Action<ICanvasWriter>>();
        public void Execute()
        {
            if (m_Used) throw new TransactionExpiredException();
            foreach (var action in m_Actions)
            {
                action.Invoke(m_BaseCanvasWriter);
            }
            m_Used = true;
        }

        public void DrawRectangle(Point p, Size s, Angle h)
        {
            if (m_Used) throw new TransactionExpiredException();
            m_Actions.Add(w => w.DrawRectangle(p, s, h));
        }

        public class TransactionExpiredException : Exception
        {
        }
    }
}
