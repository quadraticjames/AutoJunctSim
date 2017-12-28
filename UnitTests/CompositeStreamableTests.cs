using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using SimBase;

namespace UnitTests
{
    [TestFixture]
    public class CompositeStreamableTests
    {
        [Test]
        public void AccessingUnassignedRegionThrowsException()
        {
            var compositeStreamable = new CompositeStreamable<int>();
            compositeStreamable.Add(new DoubleStreamMoment(1), new DoubleStreamMoment(6), Substitute.For<IStreamable<int>>());
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
            var memberStreamable = Substitute.For<IStreamable<int>>();
            compositeStreamable.Add(new DoubleStreamMoment(1), new DoubleStreamMoment(6), memberStreamable);

            var streamMoments = new DoubleStreamMoment[] { new DoubleStreamMoment(1), new DoubleStreamMoment(Math.PI), new DoubleStreamMoment(5.99) };

            foreach(var moment in streamMoments)
            {
                compositeStreamable.AtMoment(moment);
            }

            var calls = memberStreamable.ReceivedCalls();
            Assert.That(calls.Select(c => c.GetArguments()), Is.EquivalentTo(streamMoments.Select(m => new object[] { m })));
        }
    }
}
