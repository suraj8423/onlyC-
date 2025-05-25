
# Constructor in C# – Quick Notes

## 🔹 What is a Constructor?

- A **constructor** is a special method in a class that is **executed when an object is created**.
- It handles:
  - **Object Initialization**
  - **Memory Allocation**
- The `new` keyword **creates the object**, while the **constructor initializes** its members.

---

## 🔹 Rules for C# Constructors

- Constructor **name must match the class name**.
- **No return type** is allowed — not even `void`.
- **Return statements with values** are not allowed inside constructors.

---

## 🔹 Characteristics of a Constructor

- Can have any **access modifier**: `public`, `private`, `protected`, etc.
- Can be:
  - **Parameterless**
  - **Parameterized**
- Can **throw exceptions** using a `throw` clause.
- Can include **logic** (all legal C# statements) but **not return with values**.
- You may use a simple `return;` statement.

---

## 🔹 Important Note

- ❌ You **cannot define a method** with the **same name as the class** in C#.
  - Doing so results in a **compile-time error**.

## Copy Constructor

```csharp
using System;
namespace ConstructorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CopyConstructor obj1 = new CopyConstructor(10);
            obj1.Display();
            CopyConstructor obj2 = new CopyConstructor(obj1);
            obj2.Display();
            Console.ReadKey();
        }
    }

    public class CopyConstructor
    {
        int x;

        //Parameterized Constructor
        public CopyConstructor(int i)
        {
            x = i;
        }

        //Copy Constructor
        public CopyConstructor(CopyConstructor obj)
        {
            x = obj.x;
        }

        public void Display()
        {
            Console.WriteLine($"Value of X = {x}");
        }
    }
}
```
