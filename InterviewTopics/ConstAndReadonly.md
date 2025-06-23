# Constants and Read-Only Variables in C#

## 📌 Overview

In C#, both **`const`** and **`readonly`** are used to declare immutable values, but they differ in **when** and **how** the values are assigned.

---

## 🔁 `const` (Constant) Variables

### ✅ Key Points:
- Declared using the `const` keyword.
- Must be initialized at the time of declaration.
- Value is **known at compile time**.
- **Implicitly static**, shared across all instances of the class.
- Cannot be modified after declaration.
- Value is **substituted directly in MSIL (Intermediate Language)** during compilation.

### 🧠 Memory & Behavior:
- Only one copy exists throughout the lifetime of the application.
- Object instantiation is **not required** to access `const` members.
- Similar to `static`, but cannot be changed at runtime.

### 📌 Syntax:
```csharp
public const double Pi = 3.14159;
```

---

## 🔐 `readonly` Variables

### ✅ Key Points:
- Declared using the `readonly` keyword.
- Can be initialized at the time of declaration or **inside a constructor**.
- Value is **known at runtime**.
- Not implicitly static.
- Can be different for each object instance (if non-static).
- Cannot be modified after assignment (either at declaration or in the constructor).
- The behavior of a read-only variable is similar to the behavior of a non-static variable. That is, it maintains a separate copy for each object. The only difference between these two is that the value of the non-static variable can be modified from outside the constructor while the value of the read-only variable cannot be modified from outside the constructor body.

### 🧠 Memory & Behavior:
- Not shared across instances unless marked `static`.
- Suitable when the value is determined based on runtime logic but should not change after initialization.

### 📌 Syntax:
```csharp
using System;
namespace ReadOnlyDemo
{
    class Program
    {
        readonly int number;

        //Initialize Readonly Variable through constructor
        public Program(int num)
        {
            number = num;
        }
        
        static void Main(string[] args)
        {
            Program obj1 = new Program(100);
            Console.WriteLine(obj1.number);

            //You cannot change the value of a readonly variable once it is initialized
            //The following statement will give us compile time error 
            //obj1.number = 20;

            Program obj2 = new Program(200);
            Console.WriteLine(obj2.number);

            Console.ReadLine();
        }
    }
}
```

---

## 🔄 Comparison: `const` vs `readonly`

| Feature                     | `const`                        | `readonly`                        |
|----------------------------|--------------------------------|----------------------------------|
| Value Known                | At compile time                | At runtime                       |
| Initialization Location    | Only at declaration            | At declaration or in constructor |
| Static Behavior            | Implicitly static              | Not static by default            |
| Allowed in Constructor     | ❌                            | ✅                                |
| Substitution in IL         | Yes                            | No                               |
| Use Case                   | Universal constants            | Runtime-fixed values             |

---

## 💡 Example

```csharp
class ConstantsDemo
{
    public const double Pi = 3.14159;               // Compile-time constant
    public static readonly string AppName = "Demo"; // Runtime constant (shared)
    public readonly DateTime CreatedAt;             // Runtime constant (per instance)

    public ConstantsDemo()
    {
        CreatedAt = DateTime.Now; // Allowed for readonly, not const
    }
}
```

---

## 🛑 Important Notes
- Trying to modify a `const` or `readonly` variable after assignment will result in a **compile-time error**.
- Use `const` for truly universal constants (e.g., mathematical constants, app-wide settings).
- Use `readonly` for values that depend on runtime computation but must remain fixed afterward.

---

```csharp
using System;
namespace ConstDemo
{
    class Program
    {
        //we need to assign a value to the const variable
        //at the time of const variable declaration else it will
        //give compile time error
        const float PI = 3.14f; //Constant Variable
        static void Main(string[] args)
        {
            //Const variables are static in nature
            //so we can access them by using class name 
            Console.WriteLine(Program.PI);
            //We can also access them directly within the same class
            Console.WriteLine(PI);

            //We can also declare a constant variable within a function
            const int Number = 10;
            Console.WriteLine(Number);

            //Once after declaration we cannot change the value 
            //of a constant variable. So, the below line gives an error
            //Number = 20;

            Console.ReadLine();
        }
    }
}

```