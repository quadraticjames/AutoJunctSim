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
    public class DoubleRangeMapTests
    {
        [Test]
        public void CorrectlyInsertsEndlessElement()
        {
            var map = new DoubleRangeMap<int>();
            map.Add(0, 5);

            Assert.That(map[0], Is.EqualTo(5));
            Assert.That(map[1000], Is.EqualTo(5));
            Assert.That(map[double.MaxValue], Is.EqualTo(5));
        }

        [Test]
        public void CorrectlyInsertsEndedElement()
        {
            var map = new DoubleRangeMap<int>();
            map.Add(1, 2, 5);

            Assert.That(map[1], Is.EqualTo(5));
            Assert.That(map[1.999], Is.EqualTo(5));
        }

        [Test]
        public void InitiallyDefaultEverywhere()
        {
            var map = new DoubleRangeMap<object>();
            foreach (var i in new double[] { double.MinValue, 0, double.MaxValue})
            {
                Assert.That(map[i], Is.Null);
            }
        }

        [Test]
        public void DefaultCanBeOverridden()
        {
            var obj = new DoubleRangeMap<int>();
            var map = new DoubleRangeMap<object>(obj);

            foreach(var i in new double[] { double.MinValue, 0, double.MaxValue})
            {
                Assert.That(map[i], Is.EqualTo(obj));
            }
        }

        [Test]
        public void InsertingEndedElementDoesNotChangeOutsideRange()
        {
            var map = new DoubleRangeMap<bool>(false);
            map.Add(0, 2, true);

            foreach (var i in new double[] { double.MinValue, -1, -0.0001, 2, double.MaxValue })
            {
                Assert.That(map[i], Is.False);
            }
        }

        [Test]
        public void InsertingEndlessElementDoesNotChangeOutsideRange()
        {
            var map = new DoubleRangeMap<bool>(false);
            map.Add(0, true);

            foreach (var i in new double[] { double.MinValue, -1, -0.0001 })
            {
                Assert.That(map[i], Is.False);
            }
        }

        [Test]
        public void CanOverwriteMultipleElements()
        {
            var map = new DoubleRangeMap<int>(0);
            map.Add(10, 20, 100);
            map.Add(20, 30, 101);
            map.Add(30, 40, 102);
            map.Add(0, 35, 200);

            foreach(var i in new double[] { 0, 10, 20, 30, 34 })
            {
                Assert.That(map[i], Is.EqualTo(200));
            }
            foreach (var i in new double[] { 35, 39, 39.999 })
            {
                Assert.That(map[i], Is.EqualTo(102));
            }
        }
    }
}
