```csharp

using System;
using System.Linq;
namespace LINQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] sequence1 = { 1, 2, 3, 4, 5 };
            int[] sequence2 = { 4, 5, 6, 7, 8 };

            var distinct = sequence1.Distinct();
            Console.WriteLine("Distinct: " + string.Join(", ", distinct));

            var union = sequence1.Union(sequence2);
            Console.WriteLine("Union: " + string.Join(", ", union));

            var intersect = sequence1.Intersect(sequence2);
            Console.WriteLine("Intersect: " + string.Join(", ", intersect));

            var except = sequence1.Except(sequence2);
            Console.WriteLine("Except: " + string.Join(", ", except));

            var concatenated = sequence1.Concat(sequence2);
            Console.WriteLine("Concat: " + string.Join(", ", concatenated));

            bool areEqual = sequence1.SequenceEqual(sequence2);
            Console.WriteLine($"SequenceEqual: {areEqual}");

            Console.ReadKey();
        }
    }
}
```