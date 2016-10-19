using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sw.EazyCounters.Tests
{
    [TestFixture]
    public class TestCounters
    {
        [Test]
        public void TestStore()
        {
            var cs = new CounterStore();
            var c1 = cs.GetCounter("c1");
            var c2 = cs.GetCounter("c2");

            Assert.AreEqual(0, c1.TotalEvents);
            Assert.AreEqual(0, c2.TotalEvents);

            c1.RegisterEvent();
            c1.RegisterEvent();
            c2.RegisterEvent();

            Assert.AreEqual(2, c1.TotalEvents);

            Assert.AreEqual(1 / 60, c1.AvgEventsPerMinute);


            Assert.AreEqual(1/24, c1.AvgEventsPerHour);

            Assert.AreEqual(1, c2.TotalEvents);


            var counterNames = cs.EnumerateCounters().OrderBy(a=>a);
            Assert.AreEqual(2, counterNames.Count());
            Assert.AreEqual("c1", counterNames.First());
            Assert.AreEqual("c2", counterNames.Last());
        }
    }
}
