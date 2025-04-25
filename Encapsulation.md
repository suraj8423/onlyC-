# 🔒 Encapsulation in C#

## 🧠 What is Encapsulation?

According to Microsoft Docs (MSDN):

> **Encapsulation** hides the internal state and functionality of an object and only allows access through a public set of functions.

---

## 🧾 Simplified Definition

Encapsulation is the **process of binding or grouping**:

- **State** (i.e., Data Members / Variables)
- **Behavior** (i.e., Member Functions / Methods)

...into a single unit such as a **Class**, **Struct**, **Interface**, or **Enum**.

> ✅ It **prevents direct access** to the internal data and forces interaction only through defined methods.

---

## 💊 Analogy

Encapsulation is like a **capsule**:

- A capsule holds its **medicine (data)** inside.
- You can't access the medicine directly.
- You must consume the capsule as a **whole unit** — just like you access an object’s functionality through its methods, not by accessing internal data directly.

---

## 🎯 Purpose

- To **hide implementation details**.
- To ensure **data integrity and security**.
- To reduce **system complexity** by providing a clear separation between internal workings and external usage.

---

## 🧪 Example in C#

```csharp
public class Employee
{
    // Private data members (State)
    private string name;
    private double salary;

    // Public methods (Behavior)
    public void SetName(string empName)
    {
        name = empName;
    }

    public string GetName()
    {
        return name;
    }

    public void SetSalary(double empSalary)
    {
        if(empSalary > 0)
            salary = empSalary;
    }

    public double GetSalary()
    {
        return salary;
    }
}
```

# 🛡️ Data Hiding in C#

## 🧠 What is Data Hiding?

**Data Hiding** (also known as **Information Hiding**) is the concept of **restricting access** to internal data from the outside world. 

> It ensures that internal object data is **not directly accessible** and can only be accessed or modified through **controlled interfaces** like methods or properties.

---

## 🎯 Purpose

- To **protect** the internal data from **unintended access or misuse**.
- To maintain the **integrity** and **security** of the data.
- To **simplify usage** by hiding complex internal logic.

---

## 🔁 Relation with Encapsulation

Data Hiding is achieved through **Encapsulation**.

> Without Encapsulation, Data Hiding is not possible.

In essence:

- **Encapsulation** is the **mechanism**.
- **Data Hiding** is the **result**.

---

## 💬 Simplified Definition

> The process of defining a class by **hiding its internal data members** from outside access and providing controlled access through **public getter and setter methods** or **properties** with proper validation is called **Encapsulation**, and this results in **Data Hiding**.

---

## 🧪 Example in C#

```csharp
public class Student
{
    private int age; // Hidden from outside

    // Controlled access
    public void SetAge(int value)
    {
        if(value > 0 && value < 150)
        {
            age = value;
        }
    }

    public int GetAge()
    {
        return age;
    }
}
```

# 🔐 How to Implement Data Hiding / Encapsulation in C#

## 🛠️ Implementation Steps

In C#, **Data Hiding** or **Data Encapsulation** is implemented using **Access Specifiers**, especially by:

1. **Declaring variables as `private`**
   - This restricts direct access to the variables from outside the class.

2. **Providing public `getter` and `setter` methods or `properties`**
   - These are used to **safely access or modify** the private variables **with proper validation**.

---

## 🔁 Why Use Encapsulation?

If we allow **direct access** to variables, we lose the opportunity to:
- Validate input before storing data.
- Control how data is retrieved or manipulated.
- Enforce rules or constraints on values.

Hence, encapsulation **protects and secures** the internal state of an object.

---

```csharp
using System;
namespace EncapsulationDemo
{
    public class Bank
    {
        //Hiding class data by declaring the variable as private
        private double balance;

        //Creating public Setter and Getter methods

        //Public Getter Method
        //This method is used to return the data stored in the balance variable
        public double GetBalance()
        {
            //add validation logic if needed
            return balance;
        }

        //Public Setter Method
        //This method is used to stored the data  in the balance variable
        public void SetBalance(double balance)
        {
            // add validation logic to check whether data is correct or not
            this.balance = balance;
        }
    }
    class Program
    {
        public static void Main()
        {
            Bank bank = new Bank();
            //You cannot access the Private Variable
            //bank.balance; //Compile Time Error

            //You can access the private variable via public setter and getter methods
            bank.SetBalance(500);
            Console.WriteLine(bank.GetBalance());
            Console.ReadKey();
        }
    }
}

```

# ✅ Advantages of Providing Variable Access via Setter and Getter Methods in C#

Encapsulation is a core concept of Object-Oriented Programming, and using **getter and setter methods** to access variables provides several benefits:

---

## 🛡️ 1. Data Validation

By using `setter` methods, we can **validate** data before assigning it to a variable.

```csharp
public void SetBalance(double amount)
{
    if (amount >= 0)
    {
        balance = amount;
    }
    else
    {
        Console.WriteLine("Invalid amount. Balance cannot be negative.");
    }
}
```

### 🧠 Summary

| Problem                          | Impact                                                 |
|----------------------------------|---------------------------------------------------------|
| ❌ No Validation                 | Invalid or harmful data can be set                      |
| ❌ Poor Maintainability          | Hard to update logic without breaking the codebase      |
| ❌ Security Issues               | Sensitive data can be accessed/modified freely          |
| ❌ Tight Coupling                | External code depends on internal class implementation  |
| ✅ Encapsulation Solves These   | By hiding data and exposing controlled interfaces       |


```csharp

using System;
namespace EncapsulationDemo
{
    public class Bank
    {
        private int Amount;
        public int GetAmount()
        {
            return Amount;
        }
        public void SetAmount(int Amount)
        {
            if (Amount > 0)
            {
                this.Amount = Amount;
            }
            else
            {
                throw new Exception("Please Pass a Positive Value");
            }
        }
    }
    class Program
    {
        public static void Main()
        {
            try
            {
                Bank bank = new Bank();
                //We cannot access the Amount Variable directly
                //bank.Amount = 50; //Compile Time Error
                //Console.WriteLine(bank.Amount); //Compile Time Error

                //Setting Positive Value
                bank.SetAmount(10);
                Console.WriteLine(bank.GetAmount());

                //Setting Negative Value
                bank.SetAmount(-150);
                Console.WriteLine(bank.GetAmount());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            Console.ReadKey();
        }
    }
}
```

```csharp
using System;
namespace EncapsulationDemo
{
    public class Bank
    {
        private double _Amount;
        public double Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                // Validate the value before storing it in the _Amount variable
                if (value < 0)
                {
                    throw new Exception("Please Pass a Positive Value");
                }
                else
                {
                    _Amount = value;
                }
            }
        }
    }
    class Program
    {
        public static void Main()
        {
            try
            {
                Bank bank = new Bank();
                //We cannot access the _Amount Variable directly
                //bank._Amount = 50; //Compile Time Error
                //Console.WriteLine(bank._Amount); //Compile Time Error

                //Setting Positive Value using public Amount Property
                bank.Amount= 10;

                //Setting the Value using public Amount Property
                Console.WriteLine(bank.Amount);
                
                //Setting Negative Value
                bank.Amount = -150;
                Console.WriteLine(bank.Amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
```
