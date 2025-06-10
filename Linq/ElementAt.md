# ElementAt and ElementAtOrDefault 

## ElementAt
```csharp
using System.Linq;
using System;
using System.Collections.Generic;
namespace LINQElementAtDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Data Source
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Using ElementAt Method
            //Fetch the Element from Index Position 1 using Method Syntax
            //ElementAt Method returns a Single Value
            int MethodSyntax = numbers.ElementAt(1);

            //Query Syntax
            int QuerySyntax = (from num in numbers
                               select num).ElementAt(1);

            //Printing the value returned by the ElementAt Method
            Console.WriteLine(MethodSyntax);

            Console.ReadLine();
        }
    }
}

What happens if the Index Value is out of the range of the collection?

using System.Linq;
using System;
using System.Collections.Generic;
namespace LINQElementAtDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Data Source
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Using ElementAt Method
            //Fetch the Element from Index Position -1 or 10 using Method Syntax
            //int MethodSyntax = numbers.ElementAt(-1);
            int MethodSyntax = numbers.ElementAt(10); // ArgumentOutOfRangeException

            //Printing the value returned by the ElementAt Method
            Console.WriteLine(MethodSyntax);

            Console.ReadLine();
        }
    }
}

What happens If we call the LINQ ElementAt method on an Empty Data Source in C#?

using System.Linq;
using System;
using System.Collections.Generic;
namespace LINQElementAtDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Data Source is Empty
            List<int> numbers = new List<int>();

            //Using ElementAt Method
            int MethodSyntax = numbers.ElementAt(1); // ArgumentOutOfRangeException

            //Printing the value returned by the ElementAt Method
            Console.WriteLine(MethodSyntax);

            Console.ReadLine();
        }
    }
}

What happens If we call the LINQ ElementAt method on a Data Source, which is Null?

using System.Linq;
using System;
using System.Collections.Generic;
namespace LINQElementAtDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Data Source is Null
            List<int> numbers = null;

            //Using ElementAt Method
            int MethodSyntax = numbers.ElementAt(1); // ArgumentNullException

            //Printing the value returned by the ElementAt Method
            Console.WriteLine(MethodSyntax);

            Console.ReadLine();
        }
    }
}
```

## ElementAtOrDefault
```csharp

using System.Linq;
using System;
using System.Collections.Generic;
namespace LINQElementAtOrDefaultDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Method Syntax
            int MethodSyntax = numbers.ElementAtOrDefault(1);

            //Query Syntax
            int QuerySyntax = (from num in numbers
                                select num).ElementAtOrDefault(1);

            Console.WriteLine(MethodSyntax);
            Console.ReadLine();
        }
    }
}

What happens if the Index Value is out of the Range of the Collection?

using System.Linq;
using System;
using System.Collections.Generic;
namespace LINQElementAtOrDefaultDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int MethodSyntax1 = numbers.ElementAtOrDefault(10);
            Console.WriteLine($"Value at Index Position 10: {MethodSyntax1}");

            int MethodSyntax2 = numbers.ElementAtOrDefault(-1);
            Console.WriteLine($"Value at Index Position -1: {MethodSyntax2}");

            Console.ReadLine();
        }
    }
}
result will be 
Value at Index Position 10 : 0
Value at Index Position -1 : 0
```