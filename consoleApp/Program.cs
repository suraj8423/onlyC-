using System;

namespace ExtensionMethod;

public static class EnumerableExtensions
{
    public static int SumOfSquares(this IEnumerable<int> numbers)
    {
        return numbers.Select(n => n * n).Sum();
    }
}