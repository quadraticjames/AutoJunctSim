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
    public class ConstantStreamableTests
    {
        [Test]
        public void AlwaysReturnsConstant()
        {
            var streamable = new ConstantStreamable<int>(99);
            foreach(var i in new double[] { 0, 1, 100, double.MaxValue})
            {
                Assert.That(streamable.AtMoment(new DoubleStreamMoment(i)), Is.EqualTo(99));
            }
        }
    }
}
