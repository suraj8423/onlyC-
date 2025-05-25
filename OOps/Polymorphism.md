from pathlib import Path

# Define the markdown content for Polymorphism in C#
markdown_content = """
# Polymorphism in C#

## What is Polymorphism?

Polymorphism is one of the fundamental concepts of Object-Oriented Programming (OOP). It refers to the ability of a function, method, or operator to behave differently based on the context or input.

The word **Polymorphism** is derived from two Greek words:
- **Poly** meaning *many*
- **Morphs** meaning *forms*

Hence, polymorphism means **"many forms"** — or the ability of one entity to take on multiple forms.

## Technical Definition

In C#, polymorphism allows methods or operators to behave differently depending on the types or number of input parameters.

### Key Characteristics
- A single function or operator can perform different tasks.
- The behavior changes based on input (type or number of parameters).
- It increases code flexibility and reusability.

## Why Use Polymorphism?
- To allow the same method or operator to behave differently in different scenarios.
- To achieve flexibility and scalability in your application.
- To reduce code duplication and increase readability.

## Real-Time Analogy

Imagine a person who is a **teacher at school**, a **parent at home**, and a **customer at a store**. The same person (entity) plays different roles (forms) in different situations — this is polymorphism.

## Summary

- Polymorphism means **many forms**.
- It allows a single method or function to behave differently based on the input.
- Helps in writing **flexible** and **maintainable** code.
- A core concept in Object-Oriented Programming (OOP) used extensively in C#.

"""

# Types of Polymorphism in C#

In C#, polymorphism can be classified into two main types:

## 1. Static Polymorphism / Compile-Time Polymorphism / Early Binding
## 2. Dynamic Polymorphism / Run-Time Polymorphism / Late Binding

Polymorphism in C# can be implemented using the following techniques:

- Method Overloading  
- Operator Overloading  
- Method Overriding  
- Method Hiding

> **Note**: While working with Polymorphism in C#, we need to understand two things:  
> What happens at the time of compilation, and what happens at the time of execution for a given method call.  
> Is the method going to be executed from the same class at run-time (as bound during compile time), or a different class?

---

## Compile-Time Polymorphism in C#

### What is Compile-Time Polymorphism?

Compile-Time Polymorphism (also called **Static Polymorphism** or **Early Binding**) occurs when the method to be invoked is determined at **compile time**.

This happens mainly through **Method Overloading** and **Operator Overloading**, where methods have the same name but different signatures.

In static polymorphism, the method behavior is decided during compilation, and the compiler binds the method call with the appropriate method definition.

---

### Example: Method Overloading in C#

```csharp
using System;

namespace MethodOverloading
{
    class Program
    {
        public void Add(int a, int b)
        {
            Console.WriteLine(a + b);
        }

        public void Add(float x, float y)
        {
            Console.WriteLine(x + y);
        }

        public void Add(string s1, string s2)
        {
            Console.WriteLine(s1 + " " + s2);
        }

        static void Main(string[] args)
        {
            Program obj = new Program();
            obj.Add(10, 20);               // Outputs: 30
            obj.Add(10.5f, 20.5f);         // Outputs: 31
            obj.Add("Pranaya", "Rout");    // Outputs: Pranaya Rout

            Console.ReadKey();
        }
    }
}
```

---

## Runtime Polymorphism in C#

### What is Runtime Polymorphism?

**Runtime Polymorphism** (also known as **Dynamic Polymorphism** or **Late Binding**) occurs when the method that gets executed is determined **at runtime**, not at compile time.

This usually happens via **Method Overriding**, where a base class defines a `virtual` method and a derived class overrides that method using the `override` keyword.

The Common Language Runtime (CLR) decides which method to invoke at runtime based on the actual object type.

---

### Key Concept:

If a method is overridden in a child class and a base class reference is used to hold the child object, then the method executed is from the child class, **decided at runtime**, not at compile time.

---

### Example: Method Overriding in C#

```csharp
using System;

namespace PolymorphismDemo
{
    class Class1
    {
        // Virtual method (can be overridden)
        public virtual void Show()
        {
            Console.WriteLine("Parent Class Show Method");
        }
    }

    class Class2 : Class1
    {
        // Overriding the base method
        public override void Show()
        {
            Console.WriteLine("Child Class Show Method");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Class1 obj1 = new Class2();  // Base class reference to derived class object
            obj1.Show();  // Resolved at runtime — calls Class2.Show()

            Console.ReadKey();
        }
    }
}
```