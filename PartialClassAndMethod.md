# Partial Class in C#

## What is a Partial Class?

Partial Class is a feature introduced in **C# 2.0** that allows us to **split the definition of a class** across **multiple physical files**, but **logically**, it is still treated as **a single class**.

A **class** where the code is written in **two or more files** is called a **Partial Class**.  
To define a partial class, we need to use the **`partial`** keyword.

---

## Key Points

- Physically divided into multiple files, but **logically treated as one unit**.
- Useful for organizing large classes into multiple files for better readability and maintainability.
- **Each file must explicitly specify** that the class is **partial** using the `partial` keyword.
- All parts are **combined together at compile time** into a **single class** by the compiler.

---

## Microsoft’s Definition

According to Microsoft:
> It is possible to split the definition of a class over two or more source files.  
> It is also possible to split the definition of a **struct** or an **interface** over multiple files (introduced later in C# 8.0).

Each source file contains a **section** of the class, struct, or interface definition, and all parts are **merged** when the application is **compiled**.

---

## Normal example 
```csharp
using System;
namespace PartialClassDemo
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public double Salary { get; set; }
        
        public void DisplayFullName()
        {
            Console.WriteLine($"Full Name is : {FirstName} {LastName}");
        }

        public void DisplayEmployeeDetails()
        {
            Console.WriteLine("Employee Details : ");
            Console.WriteLine($"First Name : {FirstName}");
            Console.WriteLine($"Last Name : {LastName}");
            Console.WriteLine($"Gender : {Gender}");
            Console.WriteLine($"Salary : {Salary}");
        }
    }
}

using System;
namespace PartialClassDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee emp = new Employee
            {
                FirstName = "Pranaya",
                LastName = "Rout",
                Salary = 100000,
                Gender = "Male"
            };
            emp.DisplayFullName();
            emp.DisplayEmployeeDetails();
            Console.ReadKey();
        }
    }
}
```

- lets split this and use partial class

```csharp
namespace PartialClassDemo
{
    public partial class PartialEmployee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public double Salary { get; set; }
    }
}

using System;
namespace PartialClassDemo
{
    public partial class PartialEmployee
    {
        public void DisplayFullName()
        {
            Console.WriteLine($"Full Name is : {FirstName} {LastName}");
        }

        public void DisplayEmployeeDetails()
        {
            Console.WriteLine("Employee Details : ");
            Console.WriteLine($"First Name : {FirstName}");
            Console.WriteLine($"Last Name : {LastName}");
            Console.WriteLine($"Gender : {Gender}");
            Console.WriteLine($"Salary : {Salary}");
        }
    }
}

using System;
namespace PartialClassDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            PartialEmployee emp = new PartialEmployee()
            {
                FirstName = "Pranaya",
                LastName = "Rout",
                Salary = 100000,
                Gender = "Male"
            };
            emp.DisplayFullName();
            emp.DisplayEmployeeDetails();
            Console.ReadKey();
        }
    }
}
```

# Rules to Follow While Working with Partial Classes

## Rule 1: Use of `partial` Keyword

- All parts of the class must use the `partial` keyword.
- All parts must be available at compile time to form the final type.

### Compilation Error:
> "Missing partial modifier. Another partial declaration of this type exists."

### Example:

#### ✅ Correct:
```csharp
public partial class Employee {}
public partial class Employee {}

 incorrect
public partial class Employee {}
public class Employee {} // Error: Missing partial modifier
```

## Rule 2: Same Access Modifiers

- All parts must have the same access modifier (e.g., `public`, `internal`).

### Compilation Error:
> "Partial declarations have conflicting accessibility modifiers."

### Example:

#### ❌ Incorrect:
```csharp
public partial class PartialEmployee {}
internal partial class PartialEmployee {} // Error
```

## Rule 3: Abstract, Sealed, and Inheritance Behavior

- If any part is declared `abstract`, the entire class becomes abstract.
- If any part is declared `sealed`, the entire class becomes sealed.
- If any part inherits from a base class, the entire class inherits that base class.

### Compilation Error:
> Trying to instantiate an abstract partial class without providing an implementation.

### Example:

#### 🧩 Abstract Example:
```csharp
public abstract partial class PartialEmployee {}
public partial class PartialEmployee {}
```

## Rule 4: No Multiple Base Classes

- Different parts must not specify different base classes.
- C# does not support multiple class inheritance.

### Compilation Error:
> "Partial declarations must not specify different base classes."

### Example:

#### ❌ Incorrect:
```csharp
public partial class PartialEmployee : Employee {}
public partial class PartialEmployee : Customer {} // Error
```

## Rule 5: Multiple Interfaces Allowed

- Different parts can implement different interfaces.
- The final class must implement all interface methods.
- **Note:** Multiple interface inheritance is allowed in C#.

### Example:

```csharp
public interface IEmployee {
    void Work();
}

public interface ICustomer {
    void Buy();
}

public partial class PartialClass : IEmployee {
    public void Work() {
        // Implementation
    }
}

public partial class PartialClass : ICustomer {
    public void Buy() {
        // Implementation
    }
}
```


# What are Partial Methods in C#?

A **partial class** may contain a **partial method**. One part of the class contains the signature of the method, and the implementation of the partial method can be defined in the same part or other parts of the partial class.

If the implementation is not supplied, then the method and all calls to the partial method are **removed** by the compiler at the time of compilation, making the method calls effectively **invisible**.

### Key Points:
- **Signature only**: The method signature is defined in one part of the class.
- **Optional Implementation**: The implementation can be provided in the same or another part of the class.
- **Compiler Behavior**: If no implementation is provided, the method is removed during compilation along with any calls to it.

### Example:

#### Signature (in one part of the class):
```csharp
public partial class Employee
{
    partial void LogAction(string action);
}
```

# Rules to Follow while Working with Partial Methods in C#

While working with **Partial Methods** in C#, there are certain rules and regulations that must be followed. Let us understand these rules one by one with examples.

## Rule 1: Access Modifiers in Partial Methods

- **Partial methods** in C# are **private by default**.
- You can **explicitly define** the access modifier, but **you cannot use** access modifiers such as `public`, `private`, `protected`, etc., along with **other modifiers** like `virtual`, `abstract`, `override`, `new`, `sealed`, or `extern`.
- If you try to use any access specifier explicitly, the compiler will throw an error.

### Compilation Error:
> "A partial method cannot have access modifiers or the virtual, abstract, override, new, sealed, or extern modifiers."

### Example:

#### ❌ Incorrect:
```csharp
public partial class Employee
{
    private partial void LogAction(string action); // Error: Cannot use 'private'
}
```

## Rule 2: Separate Declaration and Implementation

- In C#, a **partial method's declaration** and **implementation** must **not** be written together at the same time.
- The **declaration** should be at **one place**, and the **implementation** must be in a **different place** — either in the **same part** or in **another part** of the partial class.

- If you attempt to declare and implement a partial method simultaneously, you will get a **compiler error**.

### Compilation Error:
> "No defining declaration found for implementing declaration of partial method ‘PartialDemo.PartialClass.partialMethod()’."

### Example:

#### ❌ Incorrect:
```csharp
public partial class PartialClass
{
    partial void LogAction(string action)
    {
        Console.WriteLine(action);
    }
}
```

## Rule 3: Return Type Must Be Void

- A **partial method** in C# **must have** a **void** return type.
- You **cannot** define a partial method with a return type like `int`, `string`, `bool`, etc.
- If you attempt to use a non-void return type, the compiler will throw an **error**.

### Compilation Error:
> "Partial methods must have a void return type."

### Example:

#### ❌ Incorrect:
```csharp
public partial class PartialClass
{
    partial int LogAction(string action); // Error: Non-void return type not allowed
}
```

## Rule 4: Must Be Declared Inside a Partial Class or Partial Struct

- A **partial method** must be **declared inside** a **partial class** or **partial struct**.
- A **non-partial class** or **non-partial struct** **cannot** contain partial methods.
- If you try to define a partial method in a non-partial class or struct, the compiler will throw an **error**.

### Compilation Error:
> "A partial method must be declared within a partial class or partial struct."

### Example:

#### ❌ Incorrect:
```csharp
public class RegularClass
{
    partial void LogAction(string action); // Error: Not inside a partial class
}
```

## Rule 5: Signature Must Match Between Declaration and Implementation

- The **signature** (method name, parameters, etc.) of the **partial method declaration** must **exactly match** the **signature of the implementation**.
- If the signatures do not match, the compiler will throw an **error**.

### Compilation Error:
> "No defining declaration found for implementing declaration of partial method."

### Example:

#### ❌ Incorrect:
```csharp
public partial class PartialClass
{
    partial void LogAction(int id); // Declaration with one integer parameter
}

public partial class PartialClass
{
    partial void LogAction(); // Implementation without any parameters - Error
}
```

## Rule 6: Partial Method Can Be Implemented Only Once

- A **partial method** can have **only one implementation** — either in the **same part** or in **another part** of the partial class.
- If you try to **implement it more than once**, the compiler will throw an **error**.
- C# does **not** allow **multiple implementations** of the **same partial method** because it would violate the method signature rules (method overloading with identical signatures is not allowed).

### Compilation Error:
> "A partial method may not have multiple implementing declarations."

### Example:

#### ❌ Incorrect:
```csharp
public partial class PartialClass
{
    partial void LogAction();
}

public partial class PartialClass
{
    partial void LogAction()
    {
        Console.WriteLine("Implementation 1");
    }
}

public partial class PartialClass
{
    partial void LogAction()
    {
        Console.WriteLine("Implementation 2"); // Error: Multiple implementations
    }
}
```

