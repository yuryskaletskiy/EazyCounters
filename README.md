# EazyCounters
.net trivial in-process counter implementation (thread-safe)

## Installation

via nuget

```
PM> Install-Package Sw.EazyCounters
```

## Use

Take a look to unit test:

```
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
```