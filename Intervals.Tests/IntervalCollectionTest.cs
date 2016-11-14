using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Intervals.Tests
{
    public class IntervalCollectionTest
    {
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

            Assert.False(collection.Contains(-1));
            Assert.True(Enumerable.Range(0, 11).All(i => collection.Contains(i)));
            Assert.False(Enumerable.Range(11, 4).Any(i => collection.Contains(i)));
            Assert.True(Enumerable.Range(15, 6).All(i => collection.Contains(i)));
            Assert.False(collection.Contains(21));
        }
    }
}
