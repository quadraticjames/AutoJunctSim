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
    }
}
