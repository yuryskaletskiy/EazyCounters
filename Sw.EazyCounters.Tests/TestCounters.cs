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

            var c11 = cs.GetCounter("c1");
            Assert.AreEqual(2, c11.TotalEvents);

            cs.DeleteCounter("c1");
            counterNames = cs.EnumerateCounters().OrderBy(a => a);
            Assert.AreEqual(1, counterNames.Count());
            Assert.AreEqual("c2", counterNames.Last());


        }

        [Test]
        public void Test2()
        {
            var cs = new CounterStore();
            var c = cs.GetCounter("1");

            c.TotalEvents = 88;
            c.RegisterEvent();

            Assert.AreEqual(89, c.TotalEvents);

            Assert.AreEqual(null, c.Comment);
            c.Comment = "123";
            Assert.AreEqual("123", c.Comment);

        }

        [Test]
        public void TestMetricsHourSec()
        {
            var cs = new CounterStore();
            var c = cs.GetCounter("1");

            foreach (var i in Enumerable.Range(0, 60))
            {
                foreach (var h in Enumerable.Range(0, 24))
                {
                    c.RegisterEvent(new DateTime(2012, 01, 01, h, 1, i));
                }
            }

    //        Assert.AreEqual(120, c.TotalEvents);
            Assert.AreEqual(24, c.AvgEventsPerSecond);
            Assert.AreEqual(60, c.AvgEventsPerHour);

        }


        [Test]
        public void TestMetricsMin()
        {
            var cs = new CounterStore();
            var c = cs.GetCounter("1");

            foreach (var i in Enumerable.Range(0, 60))
            {
                    c.RegisterEvent(new DateTime(2012, 01, 01, 3, i, i));
                c.RegisterEvent(new DateTime(2012, 01, 01, 3, 59-i, 59-i));


            }
            Assert.AreEqual(2, c.AvgEventsPerMinute);

        }

    }

}
