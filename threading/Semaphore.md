
# Semaphore in C#

## Why Do We Need Semaphore?

In C#, we already have synchronization mechanisms like `lock`, `Monitor`, and `Mutex`:

- **Lock & Monitor**: Used for **internal threads** (threads created within the same application).
- **Mutex**: Used for **external threads** (threads from other applications or processes), and allows only **one** thread to access a resource at a time.

### But What If:
You want **more than one thread** (external or internal) to access a resource **concurrently**, but **only up to a limited number** at any time?

This is where **Semaphore** comes in.

---

## What is Semaphore?

The **Semaphore** class in C# is used to:

- **Limit** the number of threads that can access a shared resource concurrently.
- Allow **one or more threads** to access a resource **safely**, providing **thread safety** in multithreaded applications.

### Use Case:
Use a **Semaphore** when:
- You have a limited number of resources.
- You want to **restrict the number of concurrent threads** accessing the resource.

---

## How Does Semaphore Work?

- A Semaphore maintains a **count (Int32 variable)** initialized to a specific number.
- When a thread **enters** the critical section:
  - The count is **decreased by 1**.
- When a thread **exits**:
  - The count is **increased by 1**.
- If the count reaches **0**, no other threads are allowed to enter until one exits.

---

## Example:

```csharp
class Program
{
    static Semaphore semaphore = new Semaphore(2, 2); // Max 2 threads allowed

    static void AccessResource(object id)
    {
        Console.WriteLine($"Thread {id} waiting...");
        semaphore.WaitOne(); // Request access

        Console.WriteLine($"Thread {id} entered...");
        Thread.Sleep(2000); // Simulate work
        Console.WriteLine($"Thread {id} leaving...");

        semaphore.Release(); // Release access
    }

    static void Main()
    {
        for (int i = 1; i <= 5; i++)
        {
            Thread t = new Thread(AccessResource);
            t.Start(i);
        }
    }
}
```

### Output Example:
```
Thread 1 waiting...
Thread 1 entered...
Thread 2 waiting...
Thread 2 entered...
Thread 3 waiting...
Thread 4 waiting...
Thread 5 waiting...
Thread 1 leaving...
Thread 3 entered...
...
```

---

## Summary

| Synchronization Tool | Scope            | Allows Multiple Threads? | Use Case                                     |
|----------------------|------------------|---------------------------|-----------------------------------------------|
| `lock` / `Monitor`   | In-process only  | ❌ No                    | Internal threads                             |
| `Mutex`              | Cross-process    | ❌ No                    | External threads                             |
| `Semaphore`          | Cross-process    | ✅ Yes (Limited)         | Limit access to a resource for multiple threads |

---

✅ Use **Semaphore** when you want to limit the number of threads accessing a resource **at the same time**.
