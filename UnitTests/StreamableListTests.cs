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
    public class StreamableListTests
    {
        [Test]
        public void EmptyListAlwaysEmpty()
        {
            var list = new StreamableList<int>();
            foreach(var moment in new double[] { 0, 1, 100 }.Select(d => new DoubleStreamMoment(d)))
            {
                Assert.That(list.AtMoment(moment), Is.Empty);
            }
        }

        [Test]
        public void AddedItemReturned()
        {
            var list = new StreamableList<int>();
            list.AddAt(5, new DoubleStreamMoment(0));
            var expected = new int[] { 5 };
            foreach (var moment in new double[] { 0, 1, 100 }.Select(d => new DoubleStreamMoment(d)))
            {
                Assert.That(list.AtMoment(moment), Is.EquivalentTo(expected));
            }
        }

        [Test]
        public void AddedItemNotReturnedBeforeAddition()
        {
            var list = new StreamableList<int>();
            list.AddAt(5, new DoubleStreamMoment(1));
            foreach (var moment in new double[] { 0, 0.5, 0.9999 }.Select(d => new DoubleStreamMoment(d)))
            {
                Assert.That(list.AtMoment(moment), Is.Empty);
            }
        }
        
        [Test]
        public void RemovedItemReturnedBeforeRemoval()
        {
            var list = new StreamableList<int>();
            list.AddAt(5, new DoubleStreamMoment(1));
            list.RemoveAt(5, new DoubleStreamMoment(2));
            var expected = new int[] { 5 };

            foreach (var moment in new double[] { 1, 1.5, 1.999 }.Select(d => new DoubleStreamMoment(d)))
            {
                Assert.That(list.AtMoment(moment), Is.EquivalentTo(expected));
            }
        }

        [Test]
        public void RemovedItemNotReturnedAfterRemoval()
        {
            var list = new StreamableList<int>();
            list.AddAt(5, new DoubleStreamMoment(1));
            list.RemoveAt(5, new DoubleStreamMoment(2));

            foreach(var moment in new double[] { 2, 3, 100}.Select(d => new DoubleStreamMoment(d)))
            {
                Assert.That(list.AtMoment(moment), Is.Empty);
            }
        }
    }
}
