# Ref vs Out Keywords in C#:
- The REF and OUT keywords in C# are used for passing the arguments to a method as a reference type. By default, arguments are passed to a method by value. By using these REF and OUT keywords in C#, we can pass arguments by reference. In this case, any changes made to these arguments in the method body will be reflected in those variable when the control returns to the calling method.

```csharp
using System;
namespace RefvsOutDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //First Declare the Variables
            int Addition = 0;
            int Multiplication = 0;
            int Subtraction = 0;
            int Division = 0;
            //While calling the Method, decorate the ref keyword for ref arguments
            //Addition, Multiplication, Subtraction, and Division variables values will be updated by Math Function
            Math(200, 100, ref Addition, ref Multiplication, ref Subtraction, ref Division);

            Console.WriteLine($"Addition: {Addition}");
            Console.WriteLine($"Multiplication: {Multiplication}");
            Console.WriteLine($"Subtraction: {Subtraction}");
            Console.WriteLine($"Division: {Division}");
            
            Console.ReadKey();
        }

        //Declaring Method with Ref Parameters
        public static void Math(int number1, int number2, ref int Addition, 
            ref int Multiplication, ref int Subtraction, ref int Division)
        {
            Addition = number1 + number2; //This will Update the Addition variable Declared in Main Method
            Multiplication = number1 * number2; //This will Update the Multiplication variable Declared in Main Method
            Subtraction = number1 - number2; //This will Update the Subtraction variable Declared in Main Method
            Division = number1 / number2; //This will Update the Division variable Declared in Main Method
        }
    }
}
```

```csharp
using System;
namespace RefvsOutDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //First Declare the Variables
            int Addition = 0;
            int Multiplication = 0;
            int Subtraction = 0;
            int Division = 0;
            //While calling the Method, decorate the out keyword for out arguments
            //Addition, Multiplication, Subtraction, and Division variables values will be updated by Math Function
            Math(200, 100, out Addition, out Multiplication, out Subtraction, out Division);

            Console.WriteLine($"Addition: {Addition}");
            Console.WriteLine($"Multiplication: {Multiplication}");
            Console.WriteLine($"Subtraction: {Subtraction}");
            Console.WriteLine($"Division: {Division}");
            
            Console.ReadKey();
        }

        //Declaring Method with out Parameters
        public static void Math(int number1, int number2, out int Addition,
            out int Multiplication, out int Subtraction, out int Division)
        {
            Addition = number1 + number2; //This will Update the Addition variable Declared in Main Method
            Multiplication = number1 * number2; //This will Update the Multiplication variable Declared in Main Method
            Subtraction = number1 - number2; //This will Update the Subtraction variable Declared in Main Method
            Division = number1 / number2; //This will Update the Division variable Declared in Main Method
        }
    }
}
```

## What are the Differences Between OUT and REF Keyword in C#?

### Difference1: Updating the Ref and Out variables Inside the Method
- When we call a method with the “out” variable, the method has to update the out variable inside the function and it is mandatory. But this is not mandatory if you are using the ref variable. For example, please have a look at the below code. Here, we are commenting on the second update statement inside both MathRef and MathOut functions. If you notice inside the MathRef function, we are not getting any compile time errors. But inside the MathOut method, we are getting one compile time error saying “The out parameter ‘Subtraction’ must be assigned to before control leaves the current method” as shown below.

```csharp
using System;
namespace RefvsOutDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Calling the Method with the REF arguments
            int AdditionRef = 0;
            int SubtractionRef = 0;
            MathRef(200, 100, ref AdditionRef, ref SubtractionRef);
            Console.WriteLine($"AdditionRef: {AdditionRef}");
            Console.WriteLine($"SubtractionRef: {SubtractionRef}");

            //Call the Method with the OUT arguments
            int AdditionOut = 0;
            int SubtractionOut = 0;
            MathOut(200, 100, out AdditionOut, out SubtractionOut);
            Console.WriteLine($"AdditionOut: {AdditionOut}");
            Console.WriteLine($"SubtractionOut: {SubtractionOut}");

            Console.ReadKey();
        }

        //Creating Method with Ref Parameters
        public static void MathRef(int number1, int number2, ref int Addition, ref int Subtraction)
        {
            Addition = number1 + number2; //This will Update the Addition variable inside the Main method
            Subtraction = number1 - number2; //This will Update the Subtraction variable inside the Main method
        }

        //Creating Method with out Parameters
        public static void MathOut(int number1, int number2, out int Addition, out int Subtraction)
        {
            Addition = number1 + number2; //This will Update the Addition variable inside the Main method
            // Subtraction = number1 - number2; here we will get the error
        }
    }
}
```
- So, the first point that you need to keep in mind is that, if you are declaring some out variables, then it is mandatory or compulsory to initialize or update the out variables inside the method body else we will get a compiler error. But with the ref, updating the ref variable inside a method is optional.

### Difference2: Initializing the Ref and Out variables while passing to the Method
- When we are passing the ref parameter as arguments, it is mandatory to initialize the ref parameter before passing it to the method else we will get compile time error. This is because with the ref parameter, updating the value inside the method is optional. So, before passing the ref parameter, it should be initialized. On the other hand, initializing an out parameter is optional. If you are not initializing the out parameter, no problem, because the out parameter is compulsorily initialized or updated inside the method. For a better understanding, please have a look at the below code. Here, we are not initializing the second parameter. For the SubtractionOut parameter, we are not getting any error, but for SubtractionRef, we are getting a compiler error saying Use of unassigned local variable ‘SubtractionRef’ as shown below.

```csharp
using System;
namespace RefvsOutDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Calling the Method with the REF arguments
            int AdditionRef = 0;
            int SubtractionRef; // here you will get the error that it should be initialised
            MathRef(200, 100, ref AdditionRef, ref SubtractionRef);
            Console.WriteLine($"AdditionRef: {AdditionRef}");
            Console.WriteLine($"SubtractionRef: {SubtractionRef}");

            //Call the Method with the OUT arguments
            int AdditionOut = 0;
            int SubtractionOut = 0;
            MathOut(200, 100, out AdditionOut, out SubtractionOut);
            Console.WriteLine($"AdditionOut: {AdditionOut}");
            Console.WriteLine($"SubtractionOut: {SubtractionOut}");

            Console.ReadKey();
        }

        //Creating Method with Ref Parameters
        public static void MathRef(int number1, int number2, ref int Addition, ref int Subtraction)
        {
            Addition = number1 + number2; //This will Update the Addition variable inside the Main method
            Subtraction = number1 - number2; //This will Update the Subtraction variable inside the Main method
        }

        //Creating Method with out Parameters
        public static void MathOut(int number1, int number2, out int Addition, out int Subtraction)
        {
            Addition = number1 + number2; //This will Update the Addition variable inside the Main method
            Subtraction = number1 - number2; //This will Update the Subtraction variable inside the Main method
        }
    }
}
```
- So, the second important point that you need to keep in mind is that initializing the ref parameter is mandatory before passing such variables to the method while initializing the out-parameter variables is optional in C#.

### When to use REF Parameters in C#?
- You need to use the ref parameters when you want to pass some values to the function and you expect the values to be modified or updated by the function and give you back. To understand this better, please have a look at the below example. Here, we have one function called AddTen. This function takes one integer parameter and increments its value by 10. So, in situations like this, you need to use the ref parameter. So, you are passing some value and you expect that value to be modified by the function.

```csharp
using System;
namespace RefvsOutDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Use of Ref in C#
            int Number = 10;
            AddTen(ref Number);
            Console.WriteLine(Number);
            Console.ReadKey();
        }
        
        public static void AddTen(ref int Number)
        {
            Number = Number + 10;
        }
    }
}
```

### When to use the OUT Parameter in C#?
- With the OUT parameter, we are only expecting the output from the method. We don’t want to give any input. So, we need to use the out parameter, when we don’t want to pass any value to the function and we expect the function should and must update the variable and return the value. For a better understanding, please have a look at the below example. Here, we are passing two integer numbers to the Add function and we expect the Add function to update the Result out parameter.

```csharp
using System;
namespace RefvsOutDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Use of out in C#
            int Result;
            Add(10, 20, out Result);
            Console.WriteLine(Result);
            Console.ReadKey();
        }
        
        public static void Add(int num1, int num2, out int Result)
        {
            Result = num1 + num2;
        }
    }
}
```

### Changes to OUT Parameter in C# 7:
- The Out Parameter in C# never carries value into the method definition. So, it is not required to initialize the out parameter while declaring. So, here initializing the out parameter is useless. This is because the out parameter is going to be initialized by the method. Then you may have one question on your mind, if it is not required to initialize the out variables then why should we split their usage into two parts? First, declare the variable and then pass the variable to the function using the ref keyword.

- With the introduction of C# 7, now it is possible to declare the out parameters directly within the method. So, the above program can be rewritten as shown below and also gives the same output. Here, you can see that we are directly declaring the variable at the time of the method call i.e. Add(10, 20, out int Number);. This will eliminate the need to split the usage of the C# out variable into two parts.

```csharp
using System;
namespace RefvsOutDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Use of out in C#
            Add(10, 20, out int Number);
            Console.WriteLine(Number);
            Console.ReadKey();
        }
        
        public static void Add(int num1, int num2, out giint Result)
        {
            Result = num1 + num2;
        }
    }
}
```