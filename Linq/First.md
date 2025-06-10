# First and FirstOrDefault

- The LINQ First Method in C# returns the first element from a data source or a collection. If the data source or collection is empty, or if we specified a condition and with that condition, no element is found in the data source. The LINQ First method will throw an InvalidOperationException. If the Data Source is Null, then it will throw ArgumentNullException. 

## First

```csharp
using System.Linq;
using System;
using System.Collections.Generic;
namespace LINQFirstMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Fetching the First Element from the Data Source using First Method
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Using Method Syntax
            int MethodSyntax = numbers.First();

            //Query Syntax
            int QuerySyntax = (from num in numbers
                               select num).First();

            //Printing the value returned by the First Method
            Console.WriteLine(MethodSyntax);

            Console.ReadLine();
        }
    }
}
// output 1

Example to Understand LINQ First Method, which takes Predicate as a Parameter in C#
using System.Linq;
using System;
using System.Collections.Generic;
namespace LINQFirstMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Fetching the First Element from the Data Source which is Divisble by 2
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Using Method Syntax
            int MethodSyntax = numbers.First(num => num % 2 == 0);

            //Query Syntax
            int QuerySyntax = (from num in numbers
                               select num).First(num => num % 2 == 0);

            //Printing the value returned by the First Method
            Console.WriteLine(MethodSyntax);

            Console.ReadLine();
        }
    }
}
// output 2

What Happens when we call the First Method on an Empty Data Source?
using System.Linq;
using System;
using System.Collections.Generic;
namespace LINQFirstMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Empty Data Source
            List<int> numbersEmpty = new List<int>() { };
            int MethodSyntax = numbersEmpty.First();
            Console.WriteLine(MethodSyntax);
            
            Console.ReadLine();
        }
    }
}

// output InvalidOperationException

What Happens If the Specified Condition in the First Method Does not Return any Data?

using System.Linq;
using System;
using System.Collections.Generic;
namespace LINQFirstMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Specified Condition Doesnot Return Any Element
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int MethodSyntax = numbers.First(num => num > 50);
            Console.WriteLine(MethodSyntax);

            Console.ReadLine();
        }
    }
}
// output InvalidOperationException
```

## FirstOrDefault 

```csharp
using System.Linq;
using System;
using System.Collections.Generic;
namespace LINQFirstOrDefaultMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Using Method Syntax
            int MethodSyntax = numbers.FirstOrDefault();
            
            //Using Query Syntax
            int QuerySyntax = (from num in numbers
                               select num).FirstOrDefault();

            Console.WriteLine(MethodSyntax);

            Console.ReadLine();
        }
    }
}
// output 1

Example to Understand LINQ FirstOrDefault Method, which takes Predicate as a Parameter in C#

using System.Linq;
using System;
using System.Collections.Generic;
namespace LINQFirstOrDefaultMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Using Method Syntax
            int MethodSyntax = numbers.FirstOrDefault(num => num > 5);
            
            //Using Query Syntax
            int QuerySyntax = (from num in numbers
                               select num).FirstOrDefault(num => num > 5);

            Console.WriteLine(MethodSyntax);

            Console.ReadLine();
        }
    }
}
// output 6

What Happens when we call the FirstOrDefault Method on an Empty Data Source or when the condition does not satisfy any element?

using System.Linq;
using System;
using System.Collections.Generic;
namespace LINQFirstOrDefaultMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Empty Data Source
            List<int> numbersEmpty = new List<int>();

            //Using Method Syntax
            int MethodSyntax1 = numbersEmpty.FirstOrDefault();
            
            //Using Query Syntax
            int QuerySyntax1 = (from num in numbersEmpty
                               select num).FirstOrDefault();

            Console.WriteLine(MethodSyntax1);
            
            //Specified condition doesnot return any element
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Using Method Syntax
            int MethodSyntax2 = numbers.FirstOrDefault(num => num > 50);
            
            //Using Query Syntax
            int QuerySyntax2 = (from num in numbers
                               select num).FirstOrDefault(num => num > 50);

            Console.WriteLine(MethodSyntax2);

            Console.ReadLine();
        }
    }
}

// Here, it will print the values as 0 and 0. This is because the data source contains integers. The default for integers is 0.

```