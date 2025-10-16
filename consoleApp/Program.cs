using System;

namespace ExtentionMethod;

    public static class ExtentionMethods
    {
        public static int SumOfSquare(this IEnumerable<int> numbers)
        {
            return numbers.Select(x => x * x).Sum();
        }
    

    }

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        int sumOfSquares = numbers.SumOfSquare();
        Console.WriteLine($"Sum of squares: {sumOfSquares}"); // Output: Sum of squares: 55
        }
    }

