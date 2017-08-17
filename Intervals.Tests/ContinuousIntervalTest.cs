using System;
using Xunit;

namespace Intervals.Tests
{
    public class ContinuousIntervalTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Assert.Throws<ArgumentNullException>(() => ContinuousInterval.Create(null, new DummyComparable(1)));
            Assert.Throws<ArgumentNullException>(() => ContinuousInterval.Create(new DummyComparable(1), null));
            Assert.Throws<ArgumentException>(() => new ContinuousInterval<int>(2, 1));
        }

        [Theory]
        [InlineData(2, 2, 2, true)]
        [InlineData(2, 4, 3, true)]
        [InlineData(2, 4, 5, false)]
        [InlineData(2, 4, 1, false)]
        [InlineData(-2, 4, 3, true)]
        [InlineData(-2, 4, -1, true)]
        [InlineData(-4, -2, -3, true)]
        [InlineData(-4, -2, -1, false)]
        [InlineData(-4, -2, -5, false)]
        [InlineData(-4, -2, 3, false)]
        public void ContainsValue(int lowerBound, int upperBound, int value, bool result)
            => Assert.Equal(result, ContinuousInterval.Create(lowerBound, upperBound).Contains(value));

        [Theory]
        [InlineData(2, 2, 2, 2, true)]
        [InlineData(2, 4, 2, 4, true)]
        [InlineData(2, 5, 3, 4, true)]
        [InlineData(2, 4, 2, 5, false)]
        [InlineData(2, 4, 1, 4, false)]
        [InlineData(-2, 4, 2, 3, true)]
        [InlineData(-2, 4, -1, 3, true)]
        [InlineData(-3, 4, -2, -1, true)]
        [InlineData(-4, -1, -3, -2, true)]
        [InlineData(-4, -2, -1, 5, false)]
        [InlineData(-4, -2, -6, -5, false)]
        [InlineData(-4, -2, 1, 2, false)]
        public void ContainsInterval(int lowerBound, int upperBound, int otherLowerBound, int otherUpperBound, bool result)
               => Assert.Equal(result, ContinuousInterval.Create(lowerBound, upperBound).Contains(ContinuousInterval.Create(otherLowerBound, otherUpperBound)));

        [Theory]
        [InlineData(2, 2, true, 2, 2)]
        [InlineData(2, 4, true, 3, 6)]
        [InlineData(2, 4, true, 1, 6)]
        [InlineData(2, 3, false, 4, 6)]
        public void IntersectInt(int lowerBound1, int upperBound1, bool result, int lowerBound2, int upperBound2)
        {
            var interval1 = ContinuousInterval.Create(lowerBound1, upperBound1);
            var interval2 = ContinuousInterval.Create(lowerBound2, upperBound2);
            Assert.Equal(result, interval1.IntersectWith(interval2));
            Assert.Equal(result, interval2.IntersectWith(interval1));
        }

        private class DummyComparable : IComparable<DummyComparable>
        {
            private readonly int _value;

            public DummyComparable(int value)
            {
                _value = value;
            }

            public int CompareTo(DummyComparable other) => _value.CompareTo(other._value);
        }
    }
}
