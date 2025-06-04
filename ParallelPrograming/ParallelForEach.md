# `foreach` vs `Parallel.ForEach` in C#

In C#, both `foreach` and `Parallel.ForEach` are used to iterate over collections. The key difference lies in how the iterations are executed — **sequentially vs in parallel**.

---

## 🔁 `foreach`

- Executes **sequentially** (one item at a time).
- Simple, readable, and suitable for most scenarios.
- Uses a single thread.
- Safe for operations that must maintain order or depend on shared state (without locks).

```csharp
foreach (var item in collection)
{
    Process(item); // One after the other
}
```

---

## ⚡ `Parallel.ForEach`

- Executes **concurrently across multiple threads**.
- Speeds up data processing on multi-core systems.
- Uses **Task Parallel Library (TPL)** under the hood.
- Best for **CPU-bound**, **independent operations**.
- Care must be taken for **thread safety** when modifying shared resources.

```csharp
Parallel.ForEach(collection, item =>
{
    Process(item); // Multiple items at the same time
});
```

---

## 🔍 Comparison Table

| Feature                     | `foreach`                 | `Parallel.ForEach`             |
|-----------------------------|----------------------------|---------------------------------|
| Execution                   | Sequential (single thread) | Parallel (multi-threaded)       |
| Performance                 | Slower for large data      | Faster for CPU-bound tasks      |
| Threading                   | No extra threads           | Uses multiple threads           |
| Order of Execution          | Preserved                  | Not guaranteed                  |
| Thread Safety               | Safe by default            | Must handle manually            |
| Use Case                    | Simple iteration           | Heavy processing, large data    |
| Cancellation Support        | ❌ No                      | ✅ Yes (via `ParallelOptions`)   |
| Exception Handling          | Traditional `try-catch`    | AggregateException handling     |

---

## ⚠️ When to Use What?

- ✅ Use `foreach` when:
  - Task is lightweight or order matters.
  - Working with UI elements (not thread-safe).
  - Modifying shared resources.

- ✅ Use `Parallel.ForEach` when:
  - Tasks are **independent** and **CPU-intensive**.
  - You want to leverage multiple cores for better performance.
  - You can safely manage concurrency.

---

## ✅ Example: With Cancellation

```csharp
var cts = new CancellationTokenSource();
var options = new ParallelOptions { CancellationToken = cts.Token };

try
{
    Parallel.ForEach(collection, options, item =>
    {
        options.CancellationToken.ThrowIfCancellationRequested();
        Process(item);
    });
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operation was canceled.");
}
```

---

> 💡 **Note**: Always benchmark your specific scenario. `Parallel.ForEach` can be counterproductive for small or I/O-bound operations due to thread overhead.

- For giving example you can give teacher checking a copy example... and how it will help if he will hire some assistants for helping but it can create issues also if work is very less still you hire more people.