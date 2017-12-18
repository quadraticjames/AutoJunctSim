using System;
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

        private class ConstStreamable<T> : IStreamable<T>
        {
            private T m_Constant;
            public ConstStreamable(T constant)
            {
                m_Constant = constant;
            }

            public T AtMoment(IStreamMoment moment)
            {
                return m_Constant;
            }
        }

        [Test]
        public void AccessingUnassignedRegionThrowsException()
        {
            var compositeStreamable = new CompositeStreamable<int>();
            compositeStreamable.Add(new DoubleStreamMoment(1), new DoubleStreamMoment(6), new ConstStreamable<int>(5));
            Assert.That(() => compositeStreamable.AtMoment(new DoubleStreamMoment(0)), Throws.InstanceOf<ArgumentException>());
            Assert.That(() => compositeStreamable.AtMoment(new DoubleStreamMoment(0.99)), Throws.InstanceOf<ArgumentException>());
            Assert.That(() => compositeStreamable.AtMoment(new DoubleStreamMoment(6)), Throws.InstanceOf<ArgumentException>());
            Assert.That(() => compositeStreamable.AtMoment(new DoubleStreamMoment(100)), Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        public void EmptyCompStrAlwaysThrowsException()
        {
            var compositeStreamable = new CompositeStreamable<int>();
            Assert.That(() => compositeStreamable.AtMoment(new DoubleStreamMoment(0)), Throws.InstanceOf<ArgumentException>());
            Assert.That(() => compositeStreamable.AtMoment(new DoubleStreamMoment(100)), Throws.InstanceOf<ArgumentException>());
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
