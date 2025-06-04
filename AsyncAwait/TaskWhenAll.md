# How to Execute Multiple Tasks in C#

```csharp

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Main Thread Started");

            List<CreditCard> creditCards = CreditCard.GenerateCreditCards(10);
            Console.WriteLine($"Credit Card Generated : {creditCards.Count}");
           
            ProcessCreditCards(creditCards);
            
            Console.WriteLine($"Main Thread Completed");
            Console.ReadKey();
        }

        public static async void ProcessCreditCards(List<CreditCard> creditCards)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var tasks = new List<Task<string>>();

            //Processing the creditCards using foreach loop
            foreach (var creditCard in creditCards)
            {
                var response = ProcessCard(creditCard);
                tasks.Add(response);
            }

            //It will execute all the tasks concurrently
            await Task.WhenAll(tasks);

            // assusme this as ki aap promise dal de rahe vo list me aur jb tk sare promise resolve nhi ho jate vo awaited rahega.
            stopwatch.Stop();
            Console.WriteLine($"Processing of {creditCards.Count} Credit Cards Done in {stopwatch.ElapsedMilliseconds/1000.0} Seconds");
            //foreach(var item in tasks)
            //{
            //    Console.WriteLine(item.Result);
            //}
        }
        
        public static async Task<string> ProcessCard(CreditCard creditCard)
        {
            //Here we can do any API Call to Process the Credit Card
            //But for simplicity we are just delaying the execution for 1 second
            await Task.Delay(1000);
            string message = $"Credit Card Number: {creditCard.CardNumber} Name: {creditCard.Name} Processed";
            Console.WriteLine($"Credit Card Number: {creditCard.CardNumber} Processed");
            return message;
        }
    }

    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }

        public static List<CreditCard> GenerateCreditCards(int number)
        {
            List<CreditCard> creditCards = new List<CreditCard>();
            for (int i = 0; i < number; i++)
            {
                CreditCard card = new CreditCard()
                {
                    CardNumber = "10000000" + i,
                    Name = "CreditCard-" + i
                };

                creditCards.Add(card);
            }

            return creditCards;
        }
    }
}

output : Main Thread Started
Credit Card Generated : 10
Main Thread Completed
Credit Card Number: 100000000 Processed
Credit Card Number: 100000001 Processed
Credit Card Number: 100000002 Processed
Credit Card Number: 100000003 Processed
Credit Card Number: 100000004 Processed
Credit Card Number: 100000005 Processed
Credit Card Number: 100000006 Processed
Credit Card Number: 100000007 Processed
Credit Card Number: 100000008 Processed
Credit Card Number: 100000009 Processed
Processing of 10 Credit Cards Done in 1.00 Seconds

```

#### await Task.WhenAll(tasks): This statement tells that there is a list of tasks. Please wait for all of the tasks to be done before continuing with the execution of this method, and all of the tasks will be run simultaneously. As tasks contain 10 entries, all these 10 tasks are to be executed simultaneously.

## Execution without Task.WhenAll Method in C#:

```csharp

public static async void ProcessCreditCards(List<CreditCard> creditCards)
{
    var stopwatch = new Stopwatch();
    stopwatch.Start();

    foreach (var creditCard in creditCards)
    {
        var response = await ProcessCard(creditCard);
    }

    stopwatch.Stop();
    Console.WriteLine($"Processing of {creditCards.Count} Credit Cards Done in {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");
}

output : Main Thread Started
Credit Card Generated : 10
Main Thread Completed
Credit Card Number: 100000000 Processed
Credit Card Number: 100000001 Processed
Credit Card Number: 100000002 Processed
Credit Card Number: 100000003 Processed
Credit Card Number: 100000004 Processed
Credit Card Number: 100000005 Processed
Credit Card Number: 100000006 Processed
Credit Card Number: 100000007 Processed
Credit Card Number: 100000008 Processed
Credit Card Number: 100000009 Processed
Processing of 10 Credit Cards Done in 10.16 Seconds

```

## Offloading the Current Thread – Task.Run Method in C#

```csharp
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine($"Main Thread Started");

            List<CreditCard> creditCards = CreditCard.GenerateCreditCards(100000);
            Console.WriteLine($"Credit Card Generated : {creditCards.Count}");
           
            ProcessCreditCards(creditCards);
            
            Console.WriteLine($"Main Thread Completed");
            stopwatch.Start();
            Console.WriteLine($"Main Thread Execution Time {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");
            Console.ReadKey();
        }

        public static async void ProcessCreditCards(List<CreditCard> creditCards)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var tasks = new List<Task<string>>();
            
            foreach (var creditCard in creditCards)
            {
                var response = ProcessCard(creditCard);
                tasks.Add(response);
            }

            //It will execute all the tasks concurrently
            await Task.WhenAll(tasks);
            stopwatch.Stop();
            Console.WriteLine($"Processing of {creditCards.Count} Credit Cards Done in {stopwatch.ElapsedMilliseconds/1000.0} Seconds");
        }
        
        public static async Task<string> ProcessCard(CreditCard creditCard)
        {
            await Task.Delay(1000);
            string message = $"Credit Card Number: {creditCard.CardNumber} Name: {creditCard.Name} Processed";
            return message;
        }
    }

    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }

        public static List<CreditCard> GenerateCreditCards(int number)
        {
            List<CreditCard> creditCards = new List<CreditCard>();
            for (int i = 0; i < number; i++)
            {
                CreditCard card = new CreditCard()
                {
                    CardNumber = "10000000" + i,
                    Name = "CreditCard-" + i
                };

                creditCards.Add(card);
            }

            return creditCards;
        }
    }
}

output : Main Thread Started
Credit Card Generated : 100000
Main Thread Completed
Main Thread execution time 8.964 Seconds
Processing of 100000 Credit Cards Done in 9.792 Seconds
```

- You can see the Main thread taking approximately 9 seconds. Let us observe why. Please have a look at the below image. The following for each loop of our ProcessCreditCards method runs 100000 times, which will take some time, approximately 9 seconds. So, until the await Task.WhenAll(tasks) statement is called, our Main thread is frozen. As soon as we call, await Task.WhenAll(tasks) method, the thread is active and starts processing.

- We don’t want our Main thread to freeze for 9 seconds because one of the main reasons to use asynchronous programming in C# is to have a responsive UI. So, we don’t want the UI or Main thread to be frozen.

### How do we overcome the above problem?

```csharp
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine($"Main Thread Started");

            List<CreditCard> creditCards = CreditCard.GenerateCreditCards(100000);
            Console.WriteLine($"Credit Card Generated : {creditCards.Count}");
           
            ProcessCreditCards(creditCards);
            
            Console.WriteLine($"Main Thread Completed");
            stopwatch.Start();
            Console.WriteLine($"Main Thread Execution Time {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");
            Console.ReadKey();
        }

        public static async void ProcessCreditCards(List<CreditCard> creditCards)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var tasks = new List<Task<string>>();

            await Task.Run(() =>
            {
                foreach (var creditCard in creditCards)
                {
                    var response = ProcessCard(creditCard);
                    tasks.Add(response);
                }
            });
            
            //It will execute all the tasks concurrently
            await Task.WhenAll(tasks);
            stopwatch.Stop();
            Console.WriteLine($"Processing of {creditCards.Count} Credit Cards Done in {stopwatch.ElapsedMilliseconds/1000.0} Seconds");
        }
        
        public static async Task<string> ProcessCard(CreditCard creditCard)
        {
            await Task.Delay(1000);
            string message = $"Credit Card Number: {creditCard.CardNumber} Name: {creditCard.Name} Processed";
            return message;
        }
    }

    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }

        public static List<CreditCard> GenerateCreditCards(int number)
        {
            List<CreditCard> creditCards = new List<CreditCard>();
            for (int i = 0; i < number; i++)
            {
                CreditCard card = new CreditCard()
                {
                    CardNumber = "10000000" + i,
                    Name = "CreditCard-" + i
                };

                creditCards.Add(card);
            }

            return creditCards;
        }
    }
}

output : Main Thread Started
Credit Card Generated : 100000
Main Thread Completed
Main Thread execution time 0.102 Seconds
Processing of 100000 Credit Cards Done in 9.919 Seconds

```

- With the above changes in place, now run the application and observe the output shown in the image below. Now, the main thread is not frozen and is completed in milliseconds.

# Processing Large Number of Tasks in Batches with SemaphoreSlim in C#

## Problem
- Executing a huge number of tasks (e.g., 100,000) simultaneously can lead to:
  - Server overload
  - Throttling or blocking of HTTP requests
  - System crashes or timeouts

## Solution
- Use **batch processing** to limit the number of concurrent tasks.
- Utilize **`SemaphoreSlim`** in C# to control concurrency.

## Benefits of SemaphoreSlim
- Controls the number of tasks running in parallel
- Prevents overwhelming external services or internal resources
- Works well with `Task.WhenAll` for asynchronous execution

## Example Pattern

```csharp
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AsynchronousProgramming
{
    class Program
    {
        //Allowing Maximum 3 tasks to be executed at a time
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3);
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();  
            Console.WriteLine($"Main Thread Started");

            //Generating 15 Credit Cards
            List<CreditCard> creditCards = CreditCard.GenerateCreditCards(15);
            Console.WriteLine($"Credit Card Generated : {creditCards.Count}");
           
            ProcessCreditCards(creditCards);
            
            Console.WriteLine($"Main Thread Completed");
            Console.ReadKey();
        }

        public static async void ProcessCreditCards(List<CreditCard> creditCards)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var tasks = new List<Task<string>>();

            //Need to use async lambda expression
            tasks = creditCards.Select(async card =>
            {
                //This will tell if we have more than 4000 tasks are running, 
                //we are going to wait and '
                //we're going to wait until the semaphore gets released.
                await semaphoreSlim.WaitAsync();

                //Need to use await operator here as we are using asynchronous WaitAsync
                try
                {
                    return await ProcessCard(card);
                }
                finally
                {
                    //Release the semaphore
                    semaphoreSlim.Release();
                }
                
            }).ToList();
            
            //It will execute a maximum of 3 tasks at a time
            await Task.WhenAll(tasks);
            stopwatch.Stop();
            Console.WriteLine($"Processing of {creditCards.Count} Credit Cards Done in {stopwatch.ElapsedMilliseconds/1000.0} Seconds");
        }
        
        public static async Task<string> ProcessCard(CreditCard creditCard)
        {
            await Task.Delay(1000);
            string message = $"Credit Card Number: {creditCard.CardNumber} Name: {creditCard.Name} Processed";
            Console.WriteLine($"Credit Card Number: {creditCard.CardNumber} Processed");
            return message;
        }
    }

    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }

        public static List<CreditCard> GenerateCreditCards(int number)
        {
            List<CreditCard> creditCards = new List<CreditCard>();
            for (int i = 0; i < number; i++)
            {
                CreditCard card = new CreditCard()
                {
                    CardNumber = "10000000" + i,
                    Name = "CreditCard-" + i
                };

                creditCards.Add(card);
            }

            return creditCards;
        }
    }
}
```

#### Return type of task.whenAll methods

```csharp
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace AsynchronousProgramming
{
    class Program
    {
        //Allowing Maximum 3 tasks to be executed at a time
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3);
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            Console.WriteLine($"Main Thread Started");

            //Generating 15 Credit Cards
            List<CreditCard> creditCards = CreditCard.GenerateCreditCards(15);
            Console.WriteLine($"Credit Card Generated : {creditCards.Count}");

            ProcessCreditCards(creditCards);

            Console.WriteLine($"Main Thread Completed");
            Console.ReadKey();
        }

        public static async void ProcessCreditCards(List<CreditCard> creditCards)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var tasks = new List<Task<string>>();

            //Need to use async lambda expression
            tasks = creditCards.Select(async card =>
            {
                await semaphoreSlim.WaitAsync();

                try
                {
                    return await ProcessCard(card);
                }
                finally
                {
                    semaphoreSlim.Release();
                }

            }).ToList();


            string[] Responses = await Task.WhenAll(tasks);
            //var Responses = await Task.WhenAll(tasks);

            foreach (var response in Responses)
            {
                Console.WriteLine(response);
            }

            stopwatch.Stop();
            Console.WriteLine($"Processing of {creditCards.Count} Credit Cards Done in {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");
        }

        public static async Task<string> ProcessCard(CreditCard creditCard)
        {
            await Task.Delay(1000);
            string message = $"Credit Card Number: {creditCard.CardNumber} Name: {creditCard.Name} Processed";
            //Console.WriteLine($"Credit Card Number: {creditCard.CardNumber} Processed");
            return message;
        }
    }

    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }

        public static List<CreditCard> GenerateCreditCards(int number)
        {
            List<CreditCard> creditCards = new List<CreditCard>();
            for (int i = 0; i < number; i++)
            {
                CreditCard card = new CreditCard()
                {
                    CardNumber = "10000000" + i,
                    Name = "CreditCard-" + i
                };

                creditCards.Add(card);
            }

            return creditCards;
        }
    }
}
```

