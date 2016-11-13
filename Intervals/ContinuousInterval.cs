using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intervals
{
    public class ContinuousInterval<T> where T : IComparable<T>
    {
        public ContinuousInterval(T lowerBound, T upperBound)
        {
            if (lowerBound == null)
            {
                throw new ArgumentNullException(nameof(lowerBound));
            }
            else if (upperBound == null)
            {
                throw new ArgumentNullException(nameof(upperBound));
            }
            else if (upperBound.CompareTo(lowerBound) < 0)
            {
                throw new ArgumentException($"{nameof(upperBound)} must be superior or equal to {nameof(lowerBound)}", nameof(upperBound));
            }

            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        public T UpperBound { get; }

        public T LowerBound { get; }

        public bool Contains(T item) => LowerBound.CompareTo(item) <= 0 && 0 <= UpperBound.CompareTo(item);

        public bool IntersectWith(ContinuousInterval<T> other) => LowerBound.CompareTo(other.UpperBound) <= 0 && 0 <= UpperBound.CompareTo(other.LowerBound);
    }

    public static class ContinuousInterval
    {
        public static ContinuousInterval<T> Create<T>(T lowerBound, T upperBound) where T : IComparable<T>
            => new ContinuousInterval<T>(lowerBound, upperBound);
    }
}
