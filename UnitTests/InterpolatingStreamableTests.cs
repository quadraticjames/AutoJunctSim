using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SimBase;
using SpatialTypes;

namespace UnitTests
{
    [TestFixture]
    public class InterpolatingStreamableTests
    {
        private class Interpolable : IInterpolable<Interpolable>
        {
            public Interpolable() { }
            private Interpolable(double value)
            {
                Value = value;
            }
            public readonly double Value;
            public Interpolable Interpolate(Interpolable other, double fraction)
            {
                return new Interpolable(fraction);
            }
        }

        [Test]
        public void RejectsStreamMomentOutsideRange()
        {
            var streamable = new InterpolatingStreamable<Interpolable>(
                new Interpolable(), 
                new Interpolable(), 
                new DoubleStreamMoment(10), 
                new DoubleStreamMoment(20));

            Assert.That(() => streamable.AtMoment(new DoubleStreamMoment(9)), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => streamable.AtMoment(new DoubleStreamMoment(20.5)), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [TestCase(0)]
        [TestCase(0.1)]
        [TestCase(0.999999)]
        [TestCase(1)]
        public void CallsCorrectInterpolationFraction(double fraction)
        {
            var streamable = new InterpolatingStreamable<Interpolable>(
                new Interpolable(),
                new Interpolable(),
                new DoubleStreamMoment(1),
                new DoubleStreamMoment(101));

            var x = streamable.AtMoment(new DoubleStreamMoment(fraction * 100 + 1));
            Assert.That(x.Value, Is.EqualTo(fraction));
        }
    }
}
