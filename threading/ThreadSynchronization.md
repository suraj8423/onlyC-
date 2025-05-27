
# Thread Synchronization in C#

## What is Thread Synchronization?

Thread synchronization is a mechanism that ensures multiple threads do not access a shared resource simultaneously, which can lead to **data inconsistency**.

### Data Inconsistency Example
When two or more threads (e.g., Thread1 and Thread2) access a shared resource (e.g., `Resource1`) at the same time:
- If **Thread1** reads from the resource while **Thread2** writes to it, data inconsistency may occur.

### Why Synchronization?
To avoid such issues, synchronization ensures that only **one thread** can access the shared resource at any given time.

## Purpose of Synchronization
- Prevents multiple threads from accessing shared resources simultaneously.
- Ensures data consistency and correctness.
- Allows only one thread to access a **critical section** at a time.

## How is Synchronization Achieved in C#?

### 1. Using `lock` Keyword
C# provides built-in support for synchronization using the `lock` keyword.

### Syntax
```csharp
lock(object)
{
    // Critical section: code to be synchronized
    // Statement1
    // Statement2
    // ...
}
```

### Explanation
- Every object in C# has a **built-in lock**.
- When a thread enters a `lock` block, it **acquires a lock** on the specified object.
- Other threads trying to enter the same lock block must **wait** until the lock is released.
- The lock is **released automatically** when the thread exits the `lock` block.

## Key Points
- Synchronization is crucial when working with shared resources.
- Use the `lock` keyword to protect critical sections.
- Only one thread can execute the locked section at a time.

---

**Example:**
```csharp
class Counter
{
    private int count = 0;
    private object lockObject = new object();

    public void Increment()
    {
        lock(lockObject)
        {
            count++;
        }
    }
}
```

## Code example with issue
```csharp
using System;
using System.Threading;

namespace ThreadStateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            BookMyShow bookMyShow = new BookMyShow();
            Thread t1 = new Thread(bookMyShow.TicketBookig)
            {
                Name = "Thread1"
            };
            Thread t2 = new Thread(bookMyShow.TicketBookig)
            {
                Name = "Thread2"
            };
            Thread t3 = new Thread(bookMyShow.TicketBookig)
            {
                Name = "Thread3"
            };
            
            t1.Start();
            t2.Start();
            t3.Start();
            Console.ReadKey();
        }
    }

    public class BookMyShow
    {
        int AvailableTickets = 3;
        static int i = 1, j = 2, k = 3;
        public void BookTicket(string name, int wantedtickets)
        {
            if (wantedtickets <= AvailableTickets)
            {
                Console.WriteLine(wantedtickets + " booked to " + name);
                AvailableTickets = AvailableTickets - wantedtickets;
            }
            else
            {
                Console.WriteLine("No tickets Available to book");
            }
        }
        public void TicketBookig()
        {
            string name = Thread.CurrentThread.Name;
            if (name.Equals("Thread1"))
            {
                BookTicket(name, i);
            }
            else if (name.Equals("Thread2"))
            {
                BookTicket(name, j);
            }
            else
            {
                BookTicket(name, k);
            }
        }
    }
}
```

#### Code example with correct solution
```csharp
using System;
using System.Threading;

namespace ThreadStateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            BookMyShow bookMyShow = new BookMyShow();
            Thread t1 = new Thread(bookMyShow.TicketBookig)
            {
                Name = "Thread1"
            };
            Thread t2 = new Thread(bookMyShow.TicketBookig)
            {
                Name = "Thread2"
            };
            Thread t3 = new Thread(bookMyShow.TicketBookig)
            {
                Name = "Thread3"
            };
            
            t1.Start();
            t2.Start();
            t3.Start();
            Console.ReadKey();
        }
    }

    public class BookMyShow
    {
        private object lockObject = new object();

        int AvailableTickets = 3;
        static int i = 1, j = 2, k = 3;
        public void BookTicket(string name, int wantedtickets)
        {
            lock(lockObject)
            {
                if (wantedtickets <= AvailableTickets)
                {
                    Console.WriteLine(wantedtickets + " booked to " + name);
                    AvailableTickets = AvailableTickets - wantedtickets;
                }
                else
                {
                    Console.WriteLine("No tickets Available to book");
                }
            }
        }
        public void TicketBookig()
        {
            string name = Thread.CurrentThread.Name;
            if (name.Equals("Thread1"))
            {
                BookTicket(name, i);
            }
            else if (name.Equals("Thread2"))
            {
                BookTicket(name, j);
            }
            else
            {
                BookTicket(name, k);
            }
        }
    }
}
```
