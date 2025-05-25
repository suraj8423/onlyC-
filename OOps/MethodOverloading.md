# Method Overloading in C#

## What is Method Overloading?

Method Overloading (also known as Function Overloading) in C# is an approach that allows multiple methods to be defined with the same name within the same class. These methods must differ in their parameter **signature**, which includes:
- Number of parameters
- Type of parameters
- Order of parameters
- Kind of parameters (Value, `ref`, or `out`)

> **Note**: The **method signature does not include** the return type or the `params` modifier. So, you **cannot** overload methods based only on return type or `params`.

---

## Key Points

- You can define more than one method with the same name in a **single class**.
- You can also define overloaded methods in **parent and child classes**.
- Overloading enables a class to handle different types or numbers of inputs with methods of the same name.
- The **compiler** determines which overloaded version of the method to call based on the method signature used in the call.

---

## Method Signature Includes

- Method Name
- Number of Parameters
- Data Types of Parameters
- Order of Parameters
- Kind (`Value`, `ref`, or `out`) of Parameters

## Method Signature Does NOT Include

- Return Type
- `params` Modifier

---

## Example

```csharp
public class Calculator
{
    public int Add(int a, int b) => a + b;

    public float Add(float a, float b) => a + b;

    public int Add(int a, int b, int c) => a + b + c;

    public void Add(ref int a, int b) => a += b;
}
```

```csharp
using System;
namespace MethodOverloading
{
    class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();
            obj.Method(); //Invoke the 1st Method
            obj.Method(10); //Invoke the 2nd Method
            obj.Method("Hello"); //Invoke the 3rd Method
            obj.Method(10, "Hello"); //Invoke the 4th Method
            obj.Method("Hello", 10); //Invoke the 5th Method

            Console.ReadKey();
        }

        public void Method()
        {
            Console.WriteLine("1st Method");
        }
        public void Method(int i)
        {
            Console.WriteLine("2nd Method");
        }
        public void Method(string s)
        {
            Console.WriteLine("3rd Method");
        }
        public void Method(int i, string s)
        {
            Console.WriteLine("4th Method");
        }
        public void Method(string s, int i)
        {
            Console.WriteLine("5th Method");
        }    
    }
}
```

## Why Return Type is Not Considered in Method Overloading in C#

Even if the return types are different, **return types are not considered** for method overloading in C#. 

The reason is:

- The **confusion for the compiler happens at the time of method invocation**, not at the time of method completion.
- When calling a method, the **compiler needs to identify which method to start executing** based purely on the method name and its parameters (signature).
- The **return type** comes **into play only after** the method execution is complete. 
- Hence, **it is meaningless to differentiate** methods **based on return types** because the **compiler needs clarity before** it can even execute a method.

---

> ✅ **Important Point:**  
> In C#, method overloading is purely based on method **name** and **parameter signature** (number, type, order, kind) — **NOT based on return type**.

---

## Example (Invalid Overloading)

```csharp
public class Sample
{
    public int GetValue()
    {
        return 10;
    }

    public float GetValue() // Error: Type already defines a member called 'GetValue' with the same parameter types
    {
        return 10.5f;
    }
}
```
### What is Inheritance-Based Method Overloading in C#?

```csharp
using System;
namespace MethodOverloading
{
    class Class1
    {
        public void Add(int a, int b)
        {
            Console.WriteLine(a + b);
        }
        public void Add(float x, float y)
        {
            Console.WriteLine(x + y);
        }
    }
    class Class2 : Class1
    {
        public void Add(string s1, string s2)
        {
            Console.WriteLine(s1 +" "+ s2);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Class2 obj = new Class2();
            obj.Add(10, 20);
            obj.Add(10.5f, 20.7f);
            obj.Add("Pranaya", "Rout");
            Console.ReadKey();
        }
    }
}
```
### Example to Understand Constructor Method Overloading in C#

```csharp
using System;
namespace ConstructorOverloading
{
    class ConstructorOverloading
    {
        int x, y, z;
        public ConstructorOverloading(int x)
        {
            Console.WriteLine("Constructor1 Called");
            this.x = 10;
        }
        public ConstructorOverloading(int x, int y)
        {
            Console.WriteLine("Constructor2 Called");
            this.x = x;
            this.y = y;
        }
        public ConstructorOverloading(int x, int y, int z)
        {
            Console.WriteLine("Constructor3 Called");
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public void Display()
        {
            Console.WriteLine($"X={x}, Y={y}, Z={z}");
        }
    }
    class Test
    {
        static void Main(string[] args)
        {
            ConstructorOverloading obj1 = new ConstructorOverloading(10);
            obj1.Display();
            ConstructorOverloading obj2 = new ConstructorOverloading(10, 20);
            obj2.Display();
            ConstructorOverloading obj3 = new ConstructorOverloading(10, 20, 30);
            obj3.Display();
            Console.ReadKey();
        }
    }
}
```

### Method Overloading Realtime Example using C# Language:

```csharp
using System;
namespace MethodOverloading
{
    public class Logger
    {
        public static void Log(string ClassName, string MethodName, string Message)
        {
            Console.WriteLine($"DateTime: {DateTime.Now.ToString()}, ClassName: {ClassName}, MethodName:{MethodName}, Message:{Message}");
        }
        public static void Log(string uniqueId, string ClassName, string MethodName, string Message)
        {
            Console.WriteLine($"DateTime: {DateTime.Now.ToString()}, UniqueId: {uniqueId}, ClassName: {ClassName}, MethodName:{MethodName}, Message:{Message}");
        }
        public static void Log(string Message)
        {
            Console.WriteLine($"DateTime: {DateTime.Now.ToString()}, Message: {Message}");
        }
        public static void Log(string ClassName, string MethodName, Exception ex)
        {
            Console.WriteLine($"DateTime: {DateTime.Now.ToString()}, ClassName: {ClassName}, MethodName:{MethodName}, Exception Message:{ex.Message}, \nException StackTrace: {ex.StackTrace}");
        }

        //You create many overloaded versions as per your business requirements
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            string ClassName = "Program";
            string MethodName = "Main";
            string UniqueId = Guid.NewGuid().ToString();
            Logger.Log(ClassName, MethodName, "Message 1");
            Logger.Log(UniqueId, ClassName, MethodName, "Message 2");
            Logger.Log("Message 3");

            try
            {
                int Num1 = 10, Num2 = 0;
                int result = Num1 / Num2;
                Logger.Log(UniqueId, ClassName, MethodName, "Message 4");
            }
            catch(Exception ex)
            {
                Logger.Log(ClassName, MethodName, ex);
            }
            
            Console.ReadKey();
        }
    }
}
```