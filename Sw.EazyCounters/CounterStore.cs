using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.EazyCounters
{
    public class CounterStore
    {
        readonly Dictionary<string, Counter> _counters  = new Dictionary<string, Counter>();

        public string[] EnumerateCounters()
        {
            lock (_counters)
            {
                return _counters.Keys.ToArray();
            }
        }
        public Counter GetCounter(string counterName)
        {
            lock (_counters)
            {
                Counter c;
                if (!_counters.TryGetValue(counterName, out c))
                {
                    c = new Counter();
                    _counters.Add(counterName, c);
                }
                return c;
            }
        }
    }
}
