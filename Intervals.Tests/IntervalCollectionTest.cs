using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Intervals.Tests
{
    public class IntervalCollectionTest
    {
        private static List<ContinuousInterval<T>> GetPrivateInterval<T>(IntervalCollection<T> collection) where T : IComparable<T>
            => collection.GetType()
                         .GetField("_intervals", BindingFlags.NonPublic | BindingFlags.Instance)
                         .GetValue(collection) as List<ContinuousInterval<T>>;

        [Fact]
        public void GivenSimplestCaseThenEverythingWorksAsIntended()
        {
            Assert.True(new IntervalCollection<int>(2, 4).Contains(3));
            Assert.False(new IntervalCollection<int>(2, 4).Contains(1));
        }

        [Fact]
        public void GivenIntevalCollectionCreatedFromMultipleIntervalsThenAllIntervalAreCompacted()
        {
            var collection = new IntervalCollection<int>(ContinuousInterval.Create(3, 10), ContinuousInterval.Create(0, 5), ContinuousInterval.Create(15, 20));

            Assert.Equal(2, GetPrivateInterval(collection).Count);

            Assert.False(collection.Contains(-1));
            Assert.True(Enumerable.Range(0, 11).All(i => collection.Contains(i)));
            Assert.False(Enumerable.Range(11, 4).Any(i => collection.Contains(i)));
            Assert.True(Enumerable.Range(15, 6).All(i => collection.Contains(i)));
            Assert.False(collection.Contains(21));
        }

        [Fact]
        public void AddTest()
        {
            var collection = new IntervalCollection<int>(0, 2);
            Assert.Equal(1, GetPrivateInterval(collection).Count);

            collection.Add(5, 10);
            Assert.Equal(2, GetPrivateInterval(collection).Count);

            collection.Add(1, 6);
            var privateInterval = GetPrivateInterval(collection).Single();
            Assert.Equal(0, privateInterval.LowerBound);
            Assert.Equal(10, privateInterval.UpperBound);
        }
    }
}
