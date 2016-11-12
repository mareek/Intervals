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
    }
}
