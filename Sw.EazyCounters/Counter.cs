using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.EazyCounters
{
    public class Counter
    {
        private object _lock = new object();
        private object _commentLock = new object();

        private ulong _totalEvents;
        private string _comment;

        private readonly uint[] _secEvents = new uint[60];
        private readonly uint[] _minEvents = new uint[60];
        private readonly uint[] _hourEvents = new uint[24];

        private uint Sum(uint[] ar)
        {
            uint s = 0;
            foreach (var u in ar)
            {
                s += u;
            }
            return s;
            
        }

        public void RegisterEvent(DateTime? eventTime = null)
        {
            lock (_lock)
            {
                var actualEventTime = eventTime ?? DateTime.Now;

                _totalEvents++;

                _secEvents[actualEventTime.Second]++;
                _minEvents[actualEventTime.Minute]++;
                _hourEvents[actualEventTime.Hour]++;
            }
        }

        public ulong TotalEvents
        {
            get
            {
                lock (_lock)
                {
                    return _totalEvents;
                }
            }
            set {
                lock (_lock)
                {
                    _totalEvents = value;
                }
            }
        }

        public double AvgEventsPerSecond
        {
            get
            {
                lock (_lock)
                {
                    return Sum(_secEvents)/_secEvents.Length;
                }
            }
        }

        public string Comment
        {
            get
            {
                lock (_commentLock)
                {
                    return _comment;
                }
            }
            set
            {
                lock (_commentLock)
                {
                    _comment = value;
                }
            }
        }

        public double AvgEventsPerMinute
        {
            get
            {
                lock (_lock)
                {
                    return Sum(_minEvents)/_minEvents.Length;
                }
            }
        }

        public double AvgEventsPerHour
        {
            get {
                lock (_lock)
                {
                    return Sum(_hourEvents)/_hourEvents.Length;
                }
            }
        }
    }
}
