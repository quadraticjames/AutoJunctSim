using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimBase;

namespace UnitTests
{
    [TestFixture]
    public class CompositeStreamableTests
    {
        private class DoubleStreamMoment : IStreamMoment
        {
            private double m_Value;
            public double PositionInStream => m_Value;
            public DoubleStreamMoment(double value)
            {
                m_Value = value;
            }
        }

        [TestCase(0.0)]
        [TestCase(100.0)]
        [TestCase(1000000.0)]
        public void EmptyCompStrAlwaysThrowsException(double time)
        {
            var compositeStreamable = new CompositeStreamable<int>();
            Assert.That(() => compositeStreamable.AtMoment(new DoubleStreamMoment(time)), Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        public void CorrectlyInsertsEndlessElement()
        {
            var compositeStreamable = new CompositeStreamable<int>();
            compositeStreamable.Add(new DoubleStreamMoment(0), new ConstantStreamable<int>(5));
            Assert.That(compositeStreamable.AtMoment(new DoubleStreamMoment(0)), Is.EqualTo(5));
            Assert.That(compositeStreamable.AtMoment(new DoubleStreamMoment(100)), Is.EqualTo(5));
        }

        [Test]
        public void CorrectlyInsertsEndedElement()
        {
            var compositeStreamable = new CompositeStreamable<int>();
            compositeStreamable.Add(new DoubleStreamMoment(1), new DoubleStreamMoment(2), new ConstantStreamable<int>(5));
            Assert.That(() => compositeStreamable.AtMoment(new DoubleStreamMoment(0)), Throws.InstanceOf<ArgumentException>());
            Assert.That(compositeStreamable.AtMoment(new DoubleStreamMoment(1)), Is.EqualTo(5));
            Assert.That(compositeStreamable.AtMoment(new DoubleStreamMoment(1.5)), Is.EqualTo(5));
            Assert.That(() => compositeStreamable.AtMoment(new DoubleStreamMoment(2)), Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        public void CorrectlyOverwritesElement()
        {
            var compositeStreamable = new CompositeStreamable<int>();
            compositeStreamable.Add(new DoubleStreamMoment(0), new ConstantStreamable<int>(5));
            compositeStreamable.Add(new DoubleStreamMoment(1), new DoubleStreamMoment(2), new ConstantStreamable<int>(6));
            Assert.That(compositeStreamable.AtMoment(new DoubleStreamMoment(0)), Is.EqualTo(5));
            Assert.That(compositeStreamable.AtMoment(new DoubleStreamMoment(1)), Is.EqualTo(6));
            Assert.That(compositeStreamable.AtMoment(new DoubleStreamMoment(1.5)), Is.EqualTo(6));
            Assert.That(compositeStreamable.AtMoment(new DoubleStreamMoment(2)), Is.EqualTo(5));
            Assert.That(compositeStreamable.AtMoment(new DoubleStreamMoment(3)), Is.EqualTo(5));
        }
    }
}
