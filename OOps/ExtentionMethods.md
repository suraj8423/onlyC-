# Extension Methods in C#

## What are Extension Methods?

- Extension Methods were introduced in **C# 3.0**.
- They allow us to **add new methods** to an **existing class** **without modifying its source code**.
- This is particularly useful when:
  - The **source code is not available**.
  - You **don't have permission** to modify the class.

---

## Why Use Extension Methods?

- **Extend** the functionality of existing classes without:
  - Inheriting them.
  - Modifying their original source code.
- They provide a **clean** and **non-intrusive** way to enhance classes.

---

## Before Extension Methods: Inheritance Approach

- Traditionally, to **extend** a class:
  - You would create a **child class** (inheritance).
  - Add **new members** in the child class.
- **Calling Methods**:
  - You use the **object of the child class** to call both old and new methods.

---

## After Extension Methods: Direct Enhancement

- With **extension methods**:
  - You create a **new static class**.
  - Add **extension methods** that **appear** as if they are part of the original class.
- **Calling Methods**:
  - You use the **object of the original class** itself to call **both old and new methods**.

---

## Important Note

- **Inheritance** is **not possible** if a class is declared as **sealed**.
- However, **extension methods** still allow you to **add functionality** to a **sealed class**.
- Thus, extension methods provide a powerful and flexible way to extend classes, even when inheritance is blocked.

---

# How Extension Methods Work Internally in C#

**Extension methods** are a powerful feature in C# that allows you to "add" new methods to existing types **without modifying** the original type.

## Key Points:
- Extension methods are **static methods** inside a **static class**.
- The **first parameter** defines the type it extends, **prefixed with the `this` keyword**.
- When you call the extension method, the **compiler treats it** as if it were an **instance method** of the type you are extending.

---

## Example:

```csharp
public static class MyExtensions
{
    public static int WordCount(this string str)
    {
        if (string.IsNullOrEmpty(str)) return 0;
        return str.Split(' ').Length;
    }
}
```

# 📚 Using Extension Methods in C#

## How to Use Extension Methods

After defining, you can use it just like a regular instance method:

```csharp
string myString = "Hello world from ChatGPT";
int count = myString.WordCount();

Console.WriteLine($"Word count: {count}");
```
### Under the Hood — What Compiler Does

```csharp
myString.WordCount();
MyExtensions.WordCount(myString);
```
# 📚 Important Points about Extension Methods

1. **Namespace Usage**  
   You must **use the namespace** where the extension method class is defined, otherwise, the method won't appear.  
   Ensure the namespace containing your extension methods is referenced.

2. **Cannot Override Existing Methods**  
   You **cannot override existing methods** using extension methods.  
   Extension methods can only add new methods, not replace or override the behavior of existing ones.

3. **Priority of Methods**  
   Extension methods have **lower priority than instance methods**.  
   If a method with the same name already exists within the type, the instance method will be called first, overriding the extension method.

4. **Works with Interfaces**  
   Extension methods also work with **interfaces**, making them powerful for scenarios like `IEnumerable<T>` in LINQ.  
   This allows you to add custom behavior to built-in interfaces and collections.

### Examples

```csharp
// Extension method class in the namespace "MyExtensions"
namespace MyExtensions
{
    public static class StringExtensions
    {
        public static int WordCount(this string str)
        {
            return str.Split(' ').Length;
        }
    }
}

// Main program
class Program
{
    static void Main()
    {
        // Error: WordCount method won't be recognized without the proper namespace.
        // string myString = "Hello world from ChatGPT";
        // int count = myString.WordCount(); // Compile-time error!

        // Correct approach: Importing the namespace
        using MyExtensions;

        string myString = "Hello world from ChatGPT";
        int count = myString.WordCount();
        Console.WriteLine($"Word count: {count}");
    }
}

```

```csharp
// String class has an existing ToUpper method
namespace MyExtensions
{
    public static class StringExtensions
    {
        public static string ToUpper(this string str)
        {
            // This won't override the built-in ToUpper method of the string class
            return $"Custom {str.ToUpper()}";
        }
    }
}

class Program
{
    static void Main()
    {
        string text = "hello";
        
        // The built-in ToUpper() method will be called, not the extension method.
        Console.WriteLine(text.ToUpper()); // Output: "HELLO"
    }
}

```
```csharp
namespace MyExtensions
{
    public static class StringExtensions
    {
        public static string Greet(this string str)
        {
            return $"Hello {str}";
        }
    }
}

class MyClass
{
    public string Greet()
    {
        return "Hello from MyClass!";
    }
}

class Program
{
    static void Main()
    {
        string name = "Alice";
        MyClass obj = new MyClass();

        // This calls the instance method inside MyClass, not the extension method
        Console.WriteLine(obj.Greet());  // Output: Hello from MyClass!
        
        // This calls the extension method on the string
        Console.WriteLine(name.Greet());  // Output: Hello Alice
    }
}

```

```csharp
namespace MyExtensions
{
    public static class EnumerableExtensions
    {
        // Extension method to calculate the sum of squares
        public static int SumOfSquares(this IEnumerable<int> numbers)
        {
            return numbers.Select(x => x * x).Sum();
        }
    }
}

class Program
{
    static void Main()
    {
        IEnumerable<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        
        // Using the extension method on IEnumerable<T>
        int result = numbers.SumOfSquares();
        Console.WriteLine($"Sum of squares: {result}");  // Output: 55
    }
}

```

## Extension Methods with Interfaces (Example with IEnumerable<T>)

```csharp
public static class EnumerableExtensions
{
    public static int SumOfSquares(this IEnumerable<int> source)
    {
        int sum = 0;
        foreach (var number in source)
        {
            sum += number * number;
        }
        return sum;
    }
}


var numbers = new List<int> { 1, 2, 3, 4 };
int sumSquares = numbers.SumOfSquares();

Console.WriteLine($"Sum of squares: {sumSquares}");

```

- 👉 Whenever you write something like .Where(), .Select(), .OrderBy(), .GroupBy(), etc.,
you are actually calling extension methods on IEnumerable<T> or IQueryable<T>!

### Example 1: Using LINQ extension methods

```csharp
using System;
using System.Collections.Generic;
using System.Linq; // IMPORTANT: LINQ is here

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 1, 4, 7, 10, 13, 16 };

        var evenNumbers = numbers
                            .Where(n => n % 2 == 0)
                            .Select(n => n * 10)
                            .ToList();

        foreach (var num in evenNumbers)
        {
            Console.WriteLine(num);
        }
    }
}

```

### Example 2: Custom extension method with chaining
```csharp
public static class NumberExtensions
{
    public static bool IsEven(this int number)
    {
        return number % 2 == 0;
    }

    public static int Double(this int number)
    {
        return number * 2;
    }
}

int num = 6;

if (num.IsEven())
{
    int result = num.Double();
    Console.WriteLine(result);
}

```

### Example 3: Custom LINQ-Like Extension for Strings
```csharp
public static class StringExtensions
{
    public static string CapitalizeFirstLetter(this string str)
    {
        if (string.IsNullOrEmpty(str)) return str;
        return char.ToUpper(str[0]) + str.Substring(1);
    }
}

string name = "suraj";
string capitalizedName = name.CapitalizeFirstLetter();
Console.WriteLine(capitalizedName);

```

### Example 4: Extension Method on IEnumerable<T> (Collection)

```csharp
public static class EnumerableExtensions
{
    public static double AverageSquare(this IEnumerable<int> source)
    {
        if (!source.Any()) return 0;
        return source.Select(x => x * x).Average();
    }
}


var list = new List<int> { 1, 2, 3, 4 };

double avgSq = list.AverageSquare();
Console.WriteLine($"Average of squares: {avgSq}");

```