# Sealed Class in C#

## Definition
A **sealed class** is a class that **cannot be inherited** by any other class. It is defined using the `sealed` keyword.

## Purpose
Sealed classes are used to **prevent further inheritance**, typically for security, performance, or design reasons.

## Syntax
```csharp
sealed class MyClass
{
    // Class members
}
```
## Points to Remember while working with Sealed Class in C#

- A **sealed class** is completely the **opposite of an abstract class**.
- A sealed class **cannot contain any abstract methods**.
- It should be the **bottom-most class** within the inheritance hierarchy.
- A sealed class **can never be used as a base class**.
- A sealed class is specifically used to **avoid further inheritance**.
- The keyword `sealed` can be used with:
  - **Classes**
  - **Instance methods**
  - **Properties**
- **Note:** Even though a sealed class cannot be inherited, we can still **access its members** by creating an **object of the class**.

```csharp
using System;

namespace SealedClassExample;

public class Animal
{
    public virtual void MakeSound()
    {
        Console.WriteLine("Some generic animal sound");
    }
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Bark");
    }
}

public sealed class Labrador : Dog
{
    public override void MakeSound()
    {
        Console.WriteLine("Woof! I am a Labrador");
    }
}

public class SuperLabrador : Labrador
{
    // This class cannot override MakeSound() because Labrador is sealed
    // public override void MakeSound() { Console.WriteLine("Super Woof!"); } // This would cause a compile error
}
```


# Sealed Methods in C#

## Overview
In C#, a **sealed method** is a method that cannot be overridden in derived classes. This concept is used to restrict further modification of a method that has already been overridden in a derived class.

## Default Behavior
- By default, methods in C# are **non-virtual**, which means they **cannot be overridden** in child classes.
- To allow a method to be overridden, it must be marked as `virtual` in the **base class**.
- A method that overrides a virtual method in the parent class can be further **sealed** to prevent further overriding.

## Example Without `sealed`
```csharp
namespace SealedDemo
{
    class Parent
    {
        public virtual void Show() { }
    }
    class Child : Parent
    {
        public override void Show() { }
    }
    class GrandChild : Child
    {
        public override void Show() { }
    }
}
```
In the above example:
- `Parent` class declares `Show()` as `virtual`.
- `Child` overrides it using `override`.
- `GrandChild` again overrides it.

## Using `sealed` to Restrict Overriding
To prevent `GrandChild` from overriding the `Show()` method again, we use the `sealed` modifier.

```csharp
namespace SealedDemo
{
    class Parent
    {
        public virtual void Show() { }
    }
    class Child : Parent
    {
        public sealed override void Show() { }
    }
    class GrandChild : Child
    {
        // Error: cannot override inherited member 'Child.Show()' because it is sealed
        public override void Show() { }
    }
}
```

## Key Points to Remember
- Methods are non-overridable by default in C#.
- To allow a method to be overridden, mark it as `virtual` in the base class.
- Use the `sealed` modifier along with `override` in a derived class to prevent further overriding.
- A method can only be sealed **if it is an override** of a virtual method.

```csharp
using System;

namespace SealedDemo;

public class Printer
{
    public virtual void Display()
    {
        Console.WriteLine("Display Dimension : 5*5");
    }
    public virtual void Print()
    {
        Console.WriteLine("Printer is printing....\n");
    }
}

public class LaserJet : Printer
{
    public sealed override void Display()
    {
        Console.WriteLine("Display Dimension : 10*10");
    }

    public override void Print()
    {
        Console.WriteLine("LaserJet Printer is printing....\n");
    }
}

public sealed class InkJet : LaserJet
{
    public override void Print()
    {
        Console.WriteLine("InkJet Printer is printing....\n");
    }

    // The following method cannot be overridden because InkJet is sealed
    // public override void Display()
    // {
    //     Console.WriteLine("Display Dimension : 15*15");
    // }
}
```


# Sealed vs Private Methods in C#

## ✅ When to Use a `sealed` Method

In C#, a method should be declared as `sealed` **when we want to prevent subclasses from overriding the method of the superclass**. This ensures that all subclasses use the same base method logic.

- A `sealed` method can **still be inherited** by derived classes.
- A `sealed` method **cannot be overridden** in any further derived class.
- Attempting to override a `sealed` method results in a **compile-time error**.

## 🔐 Difference Between `private` and `sealed` Methods

| Feature                  | `private` Method                               | `sealed` Method                                    |
|--------------------------|-------------------------------------------------|----------------------------------------------------|
| Inheritance              | Not inherited by subclasses                    | Inherited by subclasses                            |
| Override Allowed         | Cannot override (not visible to subclass)      | Cannot override (even though it's visible)         |
| Method Accessibility     | Only accessible within the declaring class     | Accessible in subclass but override not allowed    |
| Re-definition in Subclass| Can define a new method with the same name     | Cannot redefine or override                        |

## 📌 Code Example

```csharp
using System;

namespace SealedDemo
{
    public class Class1
    {
        public virtual void Method1()
        {
            Console.WriteLine("Class1 Method1");
        }
    }

    public class Class2 : Class1
    {
        // Private Method
        private void Method2()
        {
            Console.WriteLine("Class2 Private Method2");
        }

        // Sealed Method
        public sealed override void Method1()
        {
            Console.WriteLine("Class2 Sealed Method1");
        }
    }

    public class Class3 : Class2
    {
        // Cannot override sealed Method1
        // public override void Method1()
        // {
        //     Console.WriteLine("Attempt to override sealed method");
        // }

        // Allowed: redefining Method2 because Class2.Method2 is private
        public void Method2()
        {
            Console.WriteLine("Class3 public Method2");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Class2 obj1 = new Class2();
            obj1.Method1(); // Output: Class2 Sealed Method1

            Class3 obj3 = new Class3();
            obj3.Method1(); // Output: Class2 Sealed Method1
            obj3.Method2(); // Output: Class3 public Method2

            Console.ReadKey();
        }
    }
```

## 🧠 Key Takeaways

- Use `sealed` when you want to stop further overriding of a method while still allowing it to be inherited.
- Use `private` when the method is intended to be completely hidden from derived classes.
- A `sealed` method must be an override of a base virtual method.
- You can **redefine a private method** in the child class without any conflict because it's not inherited.
