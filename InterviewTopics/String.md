# Strings in C#

## What is a String?

In C#, a string is an object of the `String` class that represents a sequence of characters.

- Strings support various operations:
  - Concatenation
  - Comparison
  - Substring extraction
  - Searching
  - Trimming
  - Replacement, etc.

---

## Strings are Reference Types

- In C#, strings are **reference types**.
- Other primitive types like `int`, `double` are **value types** (structs).

### 📌 Example:

```csharp
int x = 10; // value type
string name = "Suraj"; // reference type
```

- If you right-click on `int` in Visual Studio → **Go to Definition** → it’s a `struct`.
- If you right-click on `string` → **Go to Definition** → it’s a `class`.

Thus:

- `struct` → Value Type
- `class` → Reference Type

---

## string (small) vs String (capital)

### What’s the difference?

- `string` (lowercase) is an **alias** of `System.String`.
- `String` (uppercase) refers to the actual `System.String` **class** in the .NET Framework.

```csharp
string message = "Hello World";  // Preferred for variable declarations
String greeting = String.Concat("Hello", " ", "World");  // Used for method calls
```

### 🔁 You can use both interchangeably

But the **best practice** is:

- Use `string` (lowercase) for **declaration**
- Use `String` (capitalized) for **method calls**, such as:
  ```csharp
  String.IsNullOrEmpty(message);
  ```

---

## Summary

| Feature                  | string (alias)         | String (class)            |
|--------------------------|------------------------|----------------------------|
| Keyword/Type             | C# keyword             | .NET Framework class       |
| Namespace                | -                      | System                     |
| Interchangeable?         | ✅ Yes                  | ✅ Yes                      |
| Recommended Usage        | Variable declarations  | Static method invocations  |

---
# Strings are Immutable in C#

## 🔄 Mutable vs Immutable

- **Mutable**: Can be changed after creation.
- **Immutable**: Cannot be changed once created.

In C#, **strings are immutable**, meaning once a string object is created, it **cannot be modified**.

---

## 🔍 Understanding with Example

```csharp
string str = "DotNet";
str = "Tutorials";
```

- In the above code:
  - The first line creates a string object containing `"DotNet"`.
  - The second line **does not modify** the same object.
  - Instead, it creates a **new object** with the value `"Tutorials"` and `str` now points to this new object.
  - The `"DotNet"` object is no longer referenced and becomes **eligible for garbage collection**.

📌 Each time you assign a **new value** to a string variable:
- A **new memory location** is allocated.
- The old object remains for **garbage collection**.

---

## 🔁 Value Types are Mutable

```csharp
int number = 100;
number = 200;
```

- Here:
  - The value `100` is assigned.
  - When reassigned to `200`, it **overwrites** the same memory location.
  - Hence, value types like `int` are **mutable**.

---

## 📝 Key Points

| Type          | Behavior        | Memory Impact |
|---------------|------------------|----------------|
| String         | Immutable        | New memory object on each assignment |
| Value Type     | Mutable          | Same memory location is reused       |

✅ With **value types**, you modify the value in the same memory location.

❌ With **string types**, you always get a **new object**.

---

## 🧠 Summary

- C# strings are **immutable**.
- Modifying a string creates a **new object**.
- The old object is left for **garbage collection**.
- **Value types** like `int`, `double` are **mutable** and reuse memory.


```csharp
using System;
using System.Diagnostics;

namespace StringDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "";
            Console.WriteLine("Loop Started");
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < 30000000; i++)
            {
                str ="DotNet Tutorials";
            }
            stopwatch.Stop();

            Console.WriteLine("Loop Ended");
            Console.WriteLine("Loop Exceution Time in MS :" + stopwatch.ElapsedMilliseconds);

            Console.ReadKey();
        }
    }
}
```

- As you can see in the above output it only took 95 milliseconds. This is because in this case fresh objects are not created each time the loop executes. Now, the question that should come to your mind is why? The answer is String intern. So, let us understand string interning in detail.

## String Intern in C#:
- The String Intern in C# is a process that uses the same memory location if the value is the same. In our example, when the loop executes for the first time, it will create a fresh object and assign the value “DotNet Tutorials” to it. When the loop executes 2nd time, before creating a fresh object, it will check whether this “DotNet Tutorials” value is already there in the memory, if yes then it simply uses that memory location else it will create a new memory location. This is nothing but C# string interning.

- So, if you are running a for loop and assigning the same value again and again, then it uses string interning to improve the performance. In this case, rather than creating a new object, it uses the same memory location. But when the value changes it will create a new fresh object and assign the value to the new object.

# StringBuilder for Concatenation in C#

## 🧠 Problem with String Concatenation in C#

- C# strings are **immutable**, meaning every change creates a **new object**.
- This behavior becomes **inefficient** during **frequent concatenation** (e.g., inside a loop).

---

## ❌ Example: Concatenating Using Strings

```csharp
using System;
using System.Diagnostics;

namespace StringDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "";
            Console.WriteLine("Loop Started");
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < 30000; i++)
            {
                str = "DotNet Tutorials" + str;
            }
            stopwatch.Stop();

            Console.WriteLine("Loop Ended");
            Console.WriteLine("Loop Execution Time in MS :" + stopwatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
```

### 🕒 Performance

- The above loop took around **5473 ms** to execute.
- Why? Because:
  - Each concatenation creates a **new object**.
  - Previous objects are left for **garbage collection**.
  - This leads to high **memory allocation** and **GC overhead**.

---

## ✅ Solution: Use `StringBuilder` for Concatenation

- `StringBuilder` is designed for efficient string manipulation.
- It modifies the string **in place** without creating a new object every time.

---

## ✅ Optimized Example with `StringBuilder`

```csharp
using System;
using System.Diagnostics;
using System.Text;

namespace StringDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("Loop Started");
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < 30000; i++)
            {
                sb.Insert(0, "DotNet Tutorials");
            }
            stopwatch.Stop();

            Console.WriteLine("Loop Ended");
            Console.WriteLine("Loop Execution Time in MS :" + stopwatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
```

### 🚀 Performance Gain

- Much **faster execution time**.
- Less memory usage.
- No frequent garbage collection.

---

## 📝 Summary

| Feature             | String                    | StringBuilder               |
|---------------------|---------------------------|------------------------------|
| Mutability          | Immutable                 | Mutable                      |
| Memory Usage        | High (new object each time) | Low (in-place modification) |
| Best for            | Few concatenations        | Frequent concatenations      |
| Namespace           | System                    | System.Text                  |

🧠 **Use `StringBuilder`** when:
- You're performing repeated modifications.
- Performance and memory efficiency are important.

