using System;
using NUnit.Framework;
using SimBase;

namespace UnitTests
{
    [TestFixture]
    public class CompositeStreamableTests
    {
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
        public void AccessingAssignedRegionReturnsCorrectResult()
        {
            var compositeStreamable = new CompositeStreamable<int>();
            compositeStreamable.Add(new DoubleStreamMoment(1), new DoubleStreamMoment(6), new ConstStreamable<int>(5));
            Assert.That(compositeStreamable.AtMoment(new DoubleStreamMoment(1)), Is.EqualTo(5));
            Assert.That(compositeStreamable.AtMoment(new DoubleStreamMoment(5.99)), Is.EqualTo(5));
        }
    }
}
