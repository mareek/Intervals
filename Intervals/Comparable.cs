using System;

namespace Intervals
{
    public static class Comparable
    {
        public static bool IsGreaterThan<T>(this IComparable<T> left, T right) => left.CompareTo(right) > 0;
        public static bool IsLessThan<T>(this IComparable<T> left, T right) => left.CompareTo(right) < 0;
        public static bool IsEqualTo<T>(this IComparable<T> left, T right) => left.CompareTo(right) == 0;
        public static bool IsGreaterThanOrEqualTo<T>(this IComparable<T> left, T right) => left.CompareTo(right) >= 0;
        public static bool IsLessThanOrEqualTo<T>(this IComparable<T> left, T right) => left.CompareTo(right) <= 0;

        public static T Min<T>(T left, T right) where T : IComparable<T>
            => left.IsLessThan(right) ? left : right;

        public static T Max<T>(T left, T right) where T : IComparable<T>
            => left.IsGreaterThan(right) ? left : right;
    }
}
