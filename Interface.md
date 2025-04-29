
# 📘 Interface in C#

## 🔹 What is an Interface?

- An **interface** in C# is a **fully unimplemented class** used to define a **set of operations/methods**.
- It is often referred to as a **pure abstract class** because it contains **only abstract members** — no method implementations.
- Interfaces help achieve:
  - **Multiple inheritance** (which regular classes in C# can't do)
  - **Full abstraction** (since interfaces can't have any method bodies by default)

---

## 🔹 Key Features

- Declared using the `interface` keyword.
- Contains:
  - Method signatures
  - Properties
  - Events
  - Indexers
- **Cannot contain:**
  - Fields
  - Constructors
  - Access modifiers on members (all are `public` by default)
- Any class or struct implementing the interface must provide an implementation for **all** its members.

---

## 🔹 Purpose and Use

- Interfaces define a **contract** or **set of rules** that a class must follow.
- They **enforce consistency** across different classes.
- Promote **code reusability** and **loosely coupled architecture**.

---

## 🔹 Syntax Example

```csharp
public interface ICalculator
{
    void Add(int x, int y);
    void Sub(int x, int y);
}
```

```csharp
public class BasicCalculator : ICalculator
{
    public void Add(int x, int y)
    {
        Console.WriteLine(x + y);
    }

    public void Sub(int x, int y)
    {
        Console.WriteLine(x - y);
    }
}
```

---

## 🔹 Comparison: Interface vs Abstract Class

| Feature                    | Interface              | Abstract Class           |
|----------------------------|------------------------|---------------------------|
| Inheritance                | Multiple               | Single                   |
| Implementation             | No method bodies       | Can have implementations |
| Fields                     | Not allowed            | Allowed                  |
| Constructors               | Not allowed            | Allowed                  |
| Access Modifiers           | Not allowed            | Allowed                  |



# ✅ Key Points About Interfaces in C#

## 🔸 Point 1: Default Access Modifier
- In an **interface**, all members are **public by default**.
- In contrast, class members are **private by default**.

## 🔸 Point 2: Members are Abstract by Default
- Every member of an interface is **abstract** by default.
- No need to use the `abstract` keyword explicitly like in abstract classes.

```csharp
public interface IExample
{
    void Add(int x, int y); // public and abstract by default
}
```

## 🔸 Point 3: Restrictions on Declarations
- Interfaces **cannot declare**:
  - Fields (variables)
  - Constructors
  - Destructors

## 🔸 Point 4: Inheritance in Interfaces
- Interfaces can **inherit from other interfaces**, similar to class inheritance.

```csharp
public interface IBase
{
    void BaseMethod();
}

public interface IDerived : IBase
{
    void DerivedMethod();
}
```

## 🔸 Point 5: Mandatory Implementation
- All members declared in an interface **must be implemented** by the implementing class.
- **No need** to use the `override` keyword like with abstract methods.

```csharp
public class MyClass : IExample
{
    public void Add(int x, int y)
    {
        Console.WriteLine(x + y);
    }
}
```

## 🔸 Point 6: Interface References
- Cannot create **instances** of an interface.
- But can create a **reference** to an interface that holds an object of a class implementing that interface.

```csharp
IExample obj = new MyClass(); // Valid
obj.Add(5, 3); // Only methods declared in the interface are accessible
```
