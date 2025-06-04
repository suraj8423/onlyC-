## Understanding Tasks in Programming

A **task** is essentially a "promise" that signifies an operation will be performed, but not necessarily immediately. The operation may take some time, and the task will be completed in the future.

In programming, tasks are often used to represent asynchronous operations. They allow the program to continue executing other code while waiting for the task to be completed. Once the task is done, the result is provided, or an error may be thrown.

Tasks are useful for handling operations like:
- I/O-bound operations (e.g., reading/writing files, network requests)
- Time-consuming computations
- Concurrent or parallel executions

Key characteristics:
- **Deferred execution**: Tasks are not executed immediately but are scheduled to run at some point in the future.
- **Asynchronous nature**: Tasks allow non-blocking code execution, meaning the program doesn't wait for the task to complete before moving on to the next operation.

In many programming languages, tasks are used in conjunction with constructs like Promises (JavaScript), async/await (C#), or future/promise (Java, Python).


```csharp
using System;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Main Method Started......");

            SomeMethod();

            Console.WriteLine("Main Method End");
            Console.ReadKey();
        }

        public async static void SomeMethod()
        {
            Console.WriteLine("Some Method Started......");

            Wait(); // here you will get warning to use await because from the Wait method you are returning a promise.

            Console.WriteLine("Some Method End");
        }

        private static async Task Wait()
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            Console.WriteLine("\n10 Seconds wait Completed\n");
        }
    }
}
```

but if you do not want to wait for the Wait operation to complete and do not want the warning also you can do one thing make Wait function return void not Task, now Wait function is not returning a promise there will be no issue.

```csharp
using System;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Main Method Started......");

            SomeMethod();

            Console.WriteLine("Main Method End");
            Console.ReadKey();
        }

        public async static void SomeMethod()
        {
            Console.WriteLine("Some Method Started......");

            Wait();

            Console.WriteLine("Some Method End");
        }

        private static async void Wait()
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            Console.WriteLine("\n10 Seconds wait Completed\n");
        }
    }
}
```
- Whenever we use Task as return type it also comes with the error(in case if happens)... but the thing is if you will not use the await keyword you will never be able to catch the error.
- So we use try catch block for that only if Task will give you any error then it will automatically redirect you to catch block.

### Note: We can catch exceptions by using a simple try-catch block. But if we never await the task, then even if we have an exception, the exception is not going to be thrown. So, if you want to be notified about the exceptions that you may have, you need to await the task.