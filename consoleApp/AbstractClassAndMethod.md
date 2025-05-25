
# Abstract Methods and Abstract Classes in C#

## What are Abstract Methods in C#?

In C#, abstract methods are methods declared within an abstract class or an interface that do not have a method body or implementation in the declaring class or interface. Instead, the responsibility for implementing the method is delegated to any concrete (non-abstract) class that derives from the abstract class or implements the interface.

A method without the body is known as the **Abstract Method**. What the method contains is only the declaration of the method. That means the abstract method contains only the declaration, no implementation.

The following method is a non-abstract method as this method contains a body:

```csharp
public void Add(int num1, int num2)
{
}
```

But without writing the method body, if we end the method with a semicolon as follows, it is called an Abstract Method:

```csharp
public void Add(int num1, int num2);
```

But remember, if you want to make any method an abstract method, then you should explicitly use the `abstract` modifier as follows. And once you use the `abstract` modifier, automatically, the method will be called an abstract method:

```csharp
public abstract void Add(int num1, int num2);
```

## What are Abstract Classes in C#?

In C#, an abstract class is a class that serves as a blueprint for other classes. Abstract classes cannot be instantiated directly, but they can be used as base classes for other classes that derive from them. Abstract classes are declared using the `abstract` keyword. They often define a common set of characteristics or behaviors that should be shared among multiple derived classes.

A class under which we define abstract methods is known as an **abstract class**. As per object-oriented programming, we need to define a method inside a class. We cannot define the abstract methods directly anywhere. We need to define the abstract method inside an abstract class only.

Suppose we must write the above `Add` abstract method inside a `Calculator` class. Then, that class must be declared using the following `abstract` modifier:

```csharp
public abstract class Calculator
{
    public abstract void Add(int num1, int num2);
}
```

So, when a class contains any abstract methods, it must and should be declared using the `abstract` modifier, and when a class is created using an `abstract` modifier, it is called an **Abstract class** in C#.

So, this is how exactly we define an abstract class and abstract methods in C#.


# Notes on Abstract Classes and Methods in C#

## Key Points

- An **abstract class** can contain:
  - **Abstract methods**
  - **Non-abstract (concrete) methods**
- A **non-abstract class** can only contain **non-abstract methods**.
- An **abstract class** serves as a **blueprint** for other classes and **cannot be instantiated**.

## Implementation of Abstract Methods

- **Child classes** must implement **all abstract methods** of the **abstract parent class**.
- Only after implementing all abstract methods, the child class can **consume the non-abstract methods** of the parent class.

## Inheritance Behavior

- In regular inheritance, child classes can directly use parent class members.
- In case of abstract classes, child classes are **restricted** from using non-abstract methods **until** they implement all abstract methods.

## Real-life Analogy

- **Father promises son a laptop** if he scores **90% in exams**.
- The **laptop** represents the **non-abstract method**.
- The **90% marks** represent the **abstract methods**.
- The son must first fulfill the requirement (implement abstract methods) to receive the reward (use non-abstract methods).

## Important Notes

- Use the **`abstract` keyword** to declare abstract classes and methods.
- **Abstract methods** define a **contract** that any derived class **must implement**.

## Summary

| Concept               | Description |
|-----------------------|-------------|
| Abstract Class        | Can have both abstract and non-abstract methods |
| Abstract Method       | Must be implemented by child classes |
| Non-Abstract Method   | Can only be used after all abstract methods are implemented |
| Abstract Keyword      | Used to declare abstract classes and methods |


# Example to Understand Abstract Class and Abstract Methods in C#

## Abstract Class Definition

```csharp
public abstract class AbsParent
{
    public void Add(int x, int y)
    {
        Console.WriteLine($"Addition of {x} and {y} is : {x + y}");
    }

    public void Sub(int x, int y)
    {
        Console.WriteLine($"Subtraction of {x} and {y} is : {x - y}");
    }

    public abstract void Mul(int x, int y);
    public abstract void Div(int x, int y);
}
```

- `Add` and `Sub` are **non-abstract methods**.
- `Mul` and `Div` are **abstract methods**, i.e., declared but not implemented.
- `AbsParent` is marked with the **`abstract` keyword**, meaning it **cannot be instantiated** directly.

## Can We Create an Instance of an Abstract Class?

- ❌ **No**, an instance of an abstract class **cannot be created**, regardless of whether it contains abstract methods or not.
- Attempting to do so will result in a **compile-time error**.
- The error occurs because calling an abstract method (which has no body) via an instance would lead to undefined behavior.

## Static Members in Abstract Class

- If the abstract class contains **static members**, they can be accessed using the class name.
- But **non-static members** require an instance — and since the abstract class itself cannot be instantiated, **only a derived (child) class can access them**.

## Implementation by Child Class

- A child class **must implement all abstract methods** of the parent class to be valid.
- Until then, it cannot use the non-abstract methods of the abstract parent class.

## Example of Invalid Child Class (Missing Implementations)

```csharp
public class AbsChild : AbsParent
{
    // Compilation error: Must implement abstract methods Mul and Div
}
```

- This results in **compile-time errors**:
  - Error 1: `Mul` not implemented.
  - Error 2: `Div` not implemented.

## Conclusion

| Concept                                | Explanation |
|----------------------------------------|-------------|
| Abstract Class                         | Cannot be instantiated |
| Abstract Methods                       | Must be implemented by derived class |
| Non-abstract Methods in Abstract Class | Can be used after implementing abstract methods |
| Static Members                         | Can be accessed via class name |


### Why Abstract Class Cannot Be Instantiated in C#?

- Its abstract methods cannot be executed because it is not a fully implemented class. If the compiler allows us to create the object for an abstract class, we can invoke the abstract method using that object, which CLR cannot execute at runtime. Hence, to restrict calling abstract methods, the compiler does not allow us to instantiate an abstract class.

```csharp
using System;
namespace AbstractClassesAndMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creating Child class instance
            AbsChild absChild = new AbsChild();

            //Creating abstract class reference pointing to child class object
            AbsParent absParent = absChild;

            //Accessing methods using reference
            absParent.Add(10, 5);
            absParent.Sub(10, 5);
            absParent.Mul(10, 5);
            absParent.Div(10, 2);

            //You cannot call the Mod method using Parent reference as it is a pure child class method
            //absParent.Mod(100, 35);
            Console.ReadKey();
        }
    }
   
    public abstract class AbsParent
    {
        public void Add(int x, int y)
        {
            Console.WriteLine($"Addition of {x} and {y} is : {x + y}");
        }
        public void Sub(int x, int y)
        {
            Console.WriteLine($"Subtraction of {x} and {y} is : {x - y}");
        }
        public abstract void Mul(int x, int y);
        public abstract void Div(int x, int y);
    }

    public class AbsChild : AbsParent
    {
        public override void Mul(int x, int y)
        {
            Console.WriteLine($"Multiplication of {x} and {y} is : {x * y}");
        }
        public override void Div(int x, int y)
        {
            Console.WriteLine($"Division of {x} and {y} is : {x / y}");
        }
        public void Mod(int x, int y)
        {
            Console.WriteLine($"Modulos of {x} and {y} is : {x % y}");
        }
    }
}

```

## Can an Anstract class with all abstract methods be used as an interface ?