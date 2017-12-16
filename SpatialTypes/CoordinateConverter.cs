using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialTypes
{
    public class CoordinateConverter
    {
        private double m_Scalar;
        private Point m_Offset;
        public CoordinateConverter(double scalar, Point offset)
        {
            m_Scalar = scalar;
            m_Offset = offset;
        }
        public Point Convert(Point p)
        {
            return (p * m_Scalar) + m_Offset;
        }

        public Size Convert(Size s)
        {
            return s * m_Scalar;
        }
    }
}
