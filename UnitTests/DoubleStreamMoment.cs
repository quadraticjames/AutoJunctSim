using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimBase;

namespace UnitTests
{

    public class DoubleStreamMoment : IStreamMoment
    {
        private double m_Value;
        public double PositionInStream => m_Value;
        public DoubleStreamMoment(double value)
        {
            m_Value = value;
        }
    }
}
