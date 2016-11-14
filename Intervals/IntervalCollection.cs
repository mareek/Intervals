using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intervals
{
    public class IntervalCollection<T> where T : IComparable<T>
    {
        private readonly List<ContinuousInterval<T>> _intervals;

        public IntervalCollection(T lowerBound, T upperBound)
            : this(ContinuousInterval.Create(lowerBound, upperBound))
        {
        }

        public IntervalCollection(params ContinuousInterval<T>[] intervals)
        {
            _intervals = intervals.OrderBy(i => i.LowerBound).ToList();
            Compact();
        }

        private void Compact()
        {
            int i = 0;
            while (i < _intervals.Count - 1)
            {
                var current = _intervals[i];
                var next = _intervals[i + 1];
                if (current.IntersectWith(next))
                {
                    _intervals[i] = ContinuousInterval.Create(current.LowerBound, next.UpperBound);
                    _intervals.RemoveAt(i + 1);
                }
                else
                {
                    i++;
                }
            }
        }

        public bool Contains(T value) => _intervals.Any(i => i.Contains(value));
    }
}
