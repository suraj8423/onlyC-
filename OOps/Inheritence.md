# Inheritence
- The process of creating a new class from an existing class such that the new class acquires all the properties and behaviors of the existing class is called inheritance. 

## Constructor Behavior in Inheritance - C#

## Parent Class Constructor Must Be Accessible

In C#, **a child class can only inherit from a parent class if the parent’s constructor is accessible**. This is because when a child class object is created, the **parent class constructor is always invoked first**, ensuring the parent class members are initialized properly.

### Why Is This Important?

If the constructor in the parent class is **not accessible** (e.g., marked as `private`), the child class cannot call it, and inheritance will result in a compilation error.

## Implicit Constructors

If a developer does **not explicitly define a constructor**, the C# compiler automatically creates an **implicit constructor** which is **public** by default.

So in the following example:

```csharp
class A
{
    // No explicit constructor defined
}

class B : A
{
    // No explicit constructor defined
}


## Rule no.2

- In inheritance, the child class can access the parent class members, but the parent classes can never access any members that are purely defined in the child class.

# Object Class in C#

Every class that is defined by us or predefined in the libraries of the language has a default parent class: the `Object` class from the `System` namespace. This means that the members of the `Object` class — `Equals()`, `GetHashCode()`, `GetType()`, and `ToString()` — are accessible from any class.

Generally, when we define a class, we assume it is not inherited from any other class. However, by default, every class implicitly inherits from the `Object` class. Therefore, `Object` is the ultimate base class for all classes defined in the Base Class Library (BCL) as well as the classes defined by us in our applications.

## Common Methods in Object Class

- `Equals(Object obj)`: Determines whether the specified object is equal to the current object.
- `GetHashCode()`: Serves as the default hash function.
- `GetType()`: Gets the `Type` of the current instance.
- `ToString()`: Returns a string that represents the current object.

## Example

```csharp
using System;

public class MyClass
{
    public int Value { get; set; }

    public void ShowInfo()
    {
        Console.WriteLine("Type: " + this.GetType());
        Console.WriteLine("Hash Code: " + this.GetHashCode());
        Console.WriteLine("String Representation: " + this.ToString());
    }
}

class Program
{
    static void Main()
    {
        MyClass obj = new MyClass { Value = 10 };
        obj.ShowInfo();
    }
}

Type: MyClass
Hash Code: 12345678
String Representation: MyClass
```


# Rule 1: Constructor Invocation in Inheritance

In object-oriented programming, **when a child class object is created**, the constructor of the **parent class is implicitly called** before the constructor of the child class — but **only if the parent class has a parameterless constructor**.

If the **parent class constructor is parameterized**, then the **child class constructor cannot implicitly call** it. In such cases, the programmer **must explicitly call** the parent class constructor using the **`base` keyword**, and pass the required arguments.

This ensures that the parent class is properly initialized before the child class begins execution.

## Example: Calling Parameterized Parent Constructor

```csharp
using System;

public class Parent
{
    public Parent(int number)
    {
        Console.WriteLine("Parent class constructor called with value: " + number);
    }
}

public class Child : Parent
{
    public Child(int number) : base(number) // Explicit call to parent constructor
    {
        Console.WriteLine("Child class constructor");
    }
}

class Program
{
    static void Main()
    {
        Child obj = new Child(10);
    }
}
```