# How to Limit Number of Concurrent Tasks in C#

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

```
- Here we are processing 100000 cards simultaneously that is not good thing right, let say you have send 100000 HTTP request to your server,the server might be unable to handle such a huge request, or if we send 100000 HTTP requests to a server, it might be blocked or down. 

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

## Does it wait for all 3 tasks to finish before letting others in?
- No, it does not wait for all 3 to complete.
- SemaphoreSlim works on a first-come, first-served basis.

- As soon as any one of the running tasks releases the semaphore (i.e., calls semaphoreSlim.Release()), one waiting task is allowed in.

- we have this line await semaphoreSlim.WaitAsync();

* That means:

- "If there’s a slot available, proceed."

- "If not, wait until someone releases a slot."

* Let’s assume the following:

- Cards 1, 2, 3 start first and each takes 1 second to process.

- Card 1 finishes in 1 second.

- As soon as Card 1 calls semaphoreSlim.Release() in its finally block...

- The next waiting card (Card 4) is immediately allowed to start.

- Cards 2 and 3 may still be processing — and that’s totally fine.

- 🔢 At most, 3 tasks will be running concurrently at any point.


## How to Handle Response when Executing Multiple Tasks using Tasks.WhenAll Method in C#?

- if we will hover on the await Task.WhenAll(tasks) we can see it is showing string[].
- And our method ProcessCard is also returning Task<string> so if you want to read all the results code will be

```csharp
string[] Responses= await Task.WhenAll(tasks);

```


# SemaphoreSlim in C#

## Why Do We Need SemaphoreSlim?

In C#, we already have several synchronization primitives:

| Synchronization Tool | Scope            | Allows Multiple Threads? | Use Case                                     |
|----------------------|------------------|---------------------------|-----------------------------------------------|
| `lock` / `Monitor`   | In-process only  | ❌ No                    | Internal threads                             |
| `Mutex`              | Cross-process    | ❌ No                    | External threads                             |
| `Semaphore`          | Cross-process    | ✅ Yes (Limited)         | Limit access for external threads            |
| `SemaphoreSlim`      | In-process only  | ✅ Yes (Limited)         | Limit access for internal threads efficiently |

- **Lock & Monitor**: One internal thread at a time.
- **Mutex**: One external thread at a time.
- **Semaphore**: Multiple external threads (configurable count).
- **SemaphoreSlim**: Multiple internal threads (configurable count), **lightweight and faster** than Semaphore.

---

## What is SemaphoreSlim Class in C#?

- The **`SemaphoreSlim`** class is used for thread synchronization **within a single application**.
- It’s a **lightweight alternative** to `Semaphore`.
- It limits the number of **internal threads** that can concurrently access a resource or pool of resources.
- It is **not suitable** for cross-process synchronization (unlike `Semaphore` and `Mutex`).

---

## How Does SemaphoreSlim Work?

- When you create a `SemaphoreSlim`, you specify:
  - **Initial Count**: Number of threads that can enter initially.
  - **Max Count**: Maximum number of concurrent threads allowed.

### Working Mechanism:
- When a thread **enters**, the count is **decremented**.
- When a thread **exits**, the count is **incremented**.
- If the count reaches **zero**, other threads will **wait (block)** until a slot is available.

---

## Important Methods

- `Wait()` / `WaitAsync()`: Used to enter the semaphore.
- `Release()`: Used to exit the semaphore.

> ❗ If multiple threads are waiting, there’s **no guarantee of order** (e.g., FIFO or LIFO) for which gets access first.

---

## Example:

```csharp
class Program
{
    static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(2); // Max 2 threads allowed

    static async Task AccessResourceAsync(int id)
    {
        Console.WriteLine($"Thread {id} waiting...");
        await semaphoreSlim.WaitAsync();

        Console.WriteLine($"Thread {id} entered...");
        await Task.Delay(2000); // Simulate work
        Console.WriteLine($"Thread {id} leaving...");

        semaphoreSlim.Release();
    }

    static async Task Main()
    {
        List<Task> tasks = new List<Task>();
        for (int i = 1; i <= 5; i++)
        {
            tasks.Add(AccessResourceAsync(i));
        }

        await Task.WhenAll(tasks);
    }
}
```

---

## Summary

- Use `SemaphoreSlim` when you need to:
  - Limit **internal** thread access.
  - Optimize performance over full `Semaphore`.
  - Work **within a single application scope**.

✅ Lightweight  
✅ Asynchronous support  
❌ Not for external/cross-process threads
