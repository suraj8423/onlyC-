
# IEnumerable<T> in C# - LINQ Notes

## 📌 What is IEnumerable<T>?
- `IEnumerable<T>` is an **interface** in `System.Collections.Generic`.
- It represents a **sequence of elements** that can be **iterated (looped)** one at a time.
- Commonly used in **LINQ queries** and **deferred execution**.

```csharp
public interface IEnumerable<out T> : IEnumerable
{
    IEnumerator<T> GetEnumerator();
}
```

---

## ✅ Key Characteristics
- Supports **forward-only** iteration.
- Uses **lazy evaluation** (deferred execution).
- **Read-only** iteration — no add/remove.
- Works with `foreach` loops.
- LINQ query operations like `.Where()`, `.Select()`, etc., return `IEnumerable<T>`.

---

## 🔄 Deferred Execution
- Execution is **delayed** until the data is actually iterated.
- Useful for **performance optimization**.

```csharp
var result = list.Where(x => x > 10); // No execution here(here only query is generated but not executed)
foreach (var item in result)          // Execution happens here(here the execution of query will take place)
{
    Console.WriteLine(item);
}

Deferred Execution: An important aspect of the Select operator in LINQ is that it uses deferred execution. This means that the actual transformation of elements doesn’t happen when you define the Select call but when you iterate over the resulting sequence. This can be important for performance, especially with large data sets or complex queries.
```

---

## 🛠️ Common LINQ Methods That Return IEnumerable<T>
- `.Where(predicate)`
- `.Select(selector)`
- `.OrderBy(keySelector)`
- `.GroupBy(keySelector)`
- `.Take(n)`, `.Skip(n)`

---

## ⚡ IEnumerable vs List
| Feature              | IEnumerable<T>         | List<T>                    |
|----------------------|------------------------|----------------------------|
| Execution            | Lazy / Deferred        | Eager / Immediate          |
| Memory Usage         | Low (on demand)        | High (entire data loaded)  |
| Modification         | No                     | Yes                        |
| Performance          | Better for large data  | Better for frequent access |
| LINQ Compatibility   | Excellent              | Excellent                  |

---

## ✨ Examples

### 1. Basic Usage
```csharp
IEnumerable<int> numbers = Enumerable.Range(1, 5);
foreach (int number in numbers)
{
    Console.WriteLine(number);
}
```

### 2. LINQ Query with IEnumerable
```csharp
List<int> nums = new List<int> { 1, 2, 3, 4, 5 };
IEnumerable<int> evens = nums.Where(n => n % 2 == 0);
foreach (int e in evens)
{
    Console.WriteLine(e);
}
```

---

## 🚧 Limitations of IEnumerable
- Only forward iteration.
- No direct access by index.
- Cannot modify the underlying collection.
- Less efficient when repeated iteration is needed (consider `List<T>` or `ToList()`).

---

## 💡 Tips for Interviews
- `IEnumerable<T>` is good for performance and memory efficiency.
- It's ideal for pipelines and filters.
- Use `ToList()` or `ToArray()` if you need materialized data.

---

## 🔁 IEnumerable vs IEnumerator

| Concept      | IEnumerable        | IEnumerator       |
|--------------|--------------------|-------------------|
| Purpose      | Iterable collection | Iterates elements |
| Method       | `GetEnumerator()`   | `MoveNext()`, `Current`, `Reset()` |
| Usage        | foreach, LINQ       | Internal looping  |

---

## 📚 References
- Official Docs: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1
- LINQ Tutorial: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
