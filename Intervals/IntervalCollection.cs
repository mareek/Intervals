using System;
using System.Collections.Generic;
using System.Linq;

namespace Intervals
{
    public class IntervalCollection<T> where T : IComparable<T>
    {
        private readonly List<ContinuousInterval<T>> _intervals = new List<ContinuousInterval<T>>();

        public IntervalCollection(T lowerBound, T upperBound)
            : this(ContinuousInterval.Create(lowerBound, upperBound))
        {
        }

        public IntervalCollection(params ContinuousInterval<T>[] intervals)
        {
            foreach (var interval in intervals)
            {
                Add(interval);
            }
        }

        public bool Contains(T value) => _intervals.Any(i => i.Contains(value));

        public void Add(T lowerBound, T upperBound) => Add(ContinuousInterval.Create(lowerBound, upperBound));

        public void Add(ContinuousInterval<T> newInterval)
        {
            int i = 0;
            bool hasBeenAdded = false;
            while (!hasBeenAdded && i < _intervals.Count)
            {
                var current = _intervals[i];
                if (current.Contains(newInterval))
                {
                    return;
                }
                else if (newInterval.Contains(current))
                {
                    _intervals[i] = newInterval;
                    hasBeenAdded = true;
                }
                else if (newInterval.IntersectWith(current))
                {
                    _intervals[i] = ContinuousInterval.Create(Comparable.Min(newInterval.LowerBound, current.LowerBound), Comparable.Max(newInterval.UpperBound, current.UpperBound));
                    hasBeenAdded = true;
                }
                else if (newInterval.LowerBound.IsGreaterThan(current.UpperBound))
                {
                    _intervals.Insert(i + 1, newInterval);
                    hasBeenAdded = true;
                }
                else
                {
                    i++;
                }
            }

            if (!hasBeenAdded)
            {
                _intervals.Add(newInterval);
            }
            else
            {
                Compact(i);
            }
        }

        private void Compact(int at)
        {
            int atNext = at + 1;
            while (at < _intervals.Count - 1)
            {
                var current = _intervals[at];
                var next = _intervals[atNext];
                if (current.IntersectWith(next))
                {
                    _intervals[at] = ContinuousInterval.Create(current.LowerBound, next.UpperBound);
                    _intervals.RemoveAt(atNext);
                }
                else
                {
                    return;
                }
            }
        }


    }
}
