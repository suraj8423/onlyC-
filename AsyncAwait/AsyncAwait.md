# Asynchronous Programming Example in C#

## Code:

```csharp
using System;
using System.Threading;

namespace AsynchronousProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Method Started......");

            SomeMethod();

            Console.WriteLine("Main Method End");
            Console.ReadKey();
        }

        public static void SomeMethod()
        {
            Console.WriteLine("Some Method Started......");

            Thread.Sleep(TimeSpan.FromSeconds(10));
            Console.WriteLine("\n");
            Console.WriteLine("Some Method End");
        }
    }
}

Main Method Started......
Some Method Started......
(Some 10 seconds delay here...)
Some Method End

Main Method End
```

So here we have frozen our UI or blocked main thread. Now lets use Task.Delay();

```csharp

using System;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Method Started......");

            SomeMethod();

            Console.WriteLine("Main Method End");
            Console.ReadKey();
        }

        public async static void SomeMethod()
        {
            Console.WriteLine("Some Method Started......");

            //Thread.Sleep(TimeSpan.FromSeconds(10));
            await Task.Delay(TimeSpan.FromSeconds(10));
            Console.WriteLine("\n");
            Console.WriteLine("Some Method End");
        }
    }
}

Main Method Started......
Some Method Started......
Main Method End

(Some 10 seconds delay here...)

Some Method End

```

Here our main thread is free to perform other tasks also.
