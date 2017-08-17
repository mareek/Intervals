using System;

namespace Intervals
{
    public class ContinuousInterval<T>
        where T : IComparable<T>
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
            else if (upperBound.IsLessThan(lowerBound))
            {
                throw new ArgumentException($"{nameof(upperBound)} must be greater than or equal to {nameof(lowerBound)}", nameof(upperBound));
            }

            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        public T UpperBound { get; }

        public T LowerBound { get; }

        public bool Contains(T item) => LowerBound.IsLessThanOrEqualTo(item) && UpperBound.IsGreaterThanOrEqualTo(item);

        public bool Contains(ContinuousInterval<T> other) => Contains(other.LowerBound) && Contains(other.UpperBound);

        public bool IntersectWith(ContinuousInterval<T> other) => LowerBound.IsLessThanOrEqualTo(other.UpperBound) && UpperBound.IsGreaterThanOrEqualTo(other.LowerBound);
    }

    public static class ContinuousInterval
    {
        public static ContinuousInterval<T> Create<T>(T lowerBound, T upperBound) where T : IComparable<T>
            => new ContinuousInterval<T>(lowerBound, upperBound);
    }
}
