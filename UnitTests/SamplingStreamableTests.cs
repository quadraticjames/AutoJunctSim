using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimBase;
using SpatialTypes;
using NUnit.Framework;

namespace UnitTests
{

    [TestFixture]
    public class SamplingStreamableTests
    {
        [Test]
        public void EmptyStreamableThrowsException()
        {
            var streamable = new SamplingStreamable<Point>();
            foreach (var m in new double[] { 0, 1, 100.5 }.Select(d => new DoubleStreamMoment(d)))
            {
                Assert.That(() => streamable.AtMoment(m), Throws.InstanceOf<Exception>());
            }
        }

        [Test]
        public void InterpolatesBetweenTwoSamples()
        {
            var streamable = new SamplingStreamable<Point>();
            streamable.AddSample(new Point(0, 0), new DoubleStreamMoment(1));
            streamable.AddSample(new Point(1, 1), new DoubleStreamMoment(2));

            var initial = streamable.AtMoment(new DoubleStreamMoment(1));
            var final = streamable.AtMoment(new DoubleStreamMoment(1.99999999));
            var middle = streamable.AtMoment(new DoubleStreamMoment(1.5));

            Assert.That(initial.X, Is.EqualTo(0).Within(0.0001));
            Assert.That(middle.X, Is.EqualTo(0.5).Within(0.0001));
            Assert.That(final.X, Is.EqualTo(1).Within(0.0001));
        }
    }
}
