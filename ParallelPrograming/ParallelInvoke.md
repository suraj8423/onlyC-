# `Parallel.Invoke` in C#

`Parallel.Invoke` is a method in the Task Parallel Library (TPL) that runs multiple independent actions **concurrently** using available threads.

---

## ✅ Syntax

```csharp
Parallel.Invoke(
    () => DoTask1(),
    () => DoTask2(),
    () => {
        // Inline task
        Console.WriteLine("Task 3 running");
    }
);
```

---

## 🔍 Key Features

- Runs multiple actions **concurrently**.
- Optimized for **multi-core systems**.
- Handles **threading and load balancing** internally.
- Executes **void-returning tasks**.

---

## 💡 Use Cases

- Running multiple **independent methods** in parallel.
- Optimizing **CPU-bound tasks**.
- Simple parallelism without using `Task`, `async/await`.

---

## ⚠️ Considerations

| Concern              | Notes                                           |
|----------------------|-------------------------------------------------|
| Return values        | ❌ Not supported (`void` only)                  |
| Exception handling   | ✅ Via `AggregateException`                     |
| Thread safety        | ⚠️ Must handle manually if sharing resources   |
| Order of execution   | ❌ Not guaranteed                               |
| Best for             | ✅ Independent CPU-bound tasks                  |

---

## ✅ Example with Exception Handling

```csharp
try
{
    Parallel.Invoke(
        () => Console.WriteLine("Task 1"),
        () => throw new Exception("Task 2 failed"),
        () => Console.WriteLine("Task 3")
    );
}
catch (AggregateException ex)
{
    foreach (var e in ex.InnerExceptions)
    {
        Console.WriteLine($"Error: {e.Message}");
    }
}
```

---

> 💡 `Parallel.Invoke` is best when you have **short-lived, non-returning, parallelizable tasks** that can run independently.
