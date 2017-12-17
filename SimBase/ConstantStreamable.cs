using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimBase
{
    public class ConstantStreamable<T> : IStreamable<T>
    {
        private T m_Constant;
        public ConstantStreamable(T constant)
        {
            m_Constant = constant;
        }

        public T AtMoment(IStreamMoment moment)
        {
            return m_Constant;
        }
    }
}
