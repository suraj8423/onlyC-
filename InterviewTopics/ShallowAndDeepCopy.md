# Shallow and Deep Copy


## Shallow copy

- In the case of Shallow copy, it will create the new object from the existing object and then copy the value type fields of the current object to the new object. But in the case of reference type, it will only copy the reference, not the referred object itself. Therefore, the original and clone refer to the same object in the case of reference type.

```csharp
using System;
namespace ShallowCopyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example to Understand Shallow Copy
            //Creating Employee Object
            Employee emp1 = new Employee
            {
                Name = "Anurag",
                Department = "IT",
                EmpAddress = new Address() { address = "BBSR" }
            };

            //Creating a Clone Object from the Existing Object
            Employee emp2 = emp1.GetClone();

            //Changing Name Property of Clone Object Will Not Reflect the Existing Object
            emp2.Name = "Pranaya";

            //Changing Address Property of Clone Object Will Reflect the Changes of the Existing Object
            //This is because address is a reference type property and in the case of Shallow Copy
            //Both Clone and Existing Object will point to the Same Memory Address
            emp2.EmpAddress.address = "Mumbai";

            Console.WriteLine("Emplpyee 1: ");
            Console.WriteLine("Name: " + emp1.Name + ", Address: " + emp1.EmpAddress.address + ", Dept: " + emp1.Department);
            Console.WriteLine("Emplpyee 2: ");
            Console.WriteLine("Name: " + emp2.Name + ", Address: " + emp2.EmpAddress.address + ", Dept: " + emp2.Department);

            Console.Read();
        }
    }

    public class Employee
    {
        //Value type Property
        public string Name { get; set; }
        //Value type Property
        public string Department { get; set; }
        //Reference type Property
        public Address EmpAddress { get; set; }

        //Creating a Cloned Object of the Current Object
        public Employee GetClone()
        {
            //Both Cloned and Existing Object Point to the Same Memory Location of the Address Object
            return (Employee)this.MemberwiseClone();
        }
    }

    public class Address
    {
        public string address { get; set; }
    }
}

// output 
Employee 1:
Name: Anurag, Address: Mumbai, Dept: IT
Employee 2:
Name: Pranaya, Address: Mumbai, Dept: IT

```

## Deep Copy

- In the case of deep copy, it will create a new object from the existing object and then copy the fields of the current object to the newly created object. If the field is a value type, then a bit-by-bit copy of the field will be performed. A new copy of the referred object will be created if the field is a reference type.

```csharp
using System;
namespace DeepCopyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example to Understand Deep Copy
            //Creating Employee Object
            Employee emp1 = new Employee
            {
                Name = "Anurag",
                Department = "IT",
                EmpAddress = new Address() { address = "BBSR" }
            };

            //Creating a Clone Object from the Existing Object
            Employee emp2 = emp1.GetClone();

            //Changing Name Property of Clone Object Will Not Reflect the Existing Object
            emp2.Name = "Pranaya";

            //Changing Address Property of Clone Object Will Not Reflect the Changes in the Existing Object
            //This is because of Deep Copy
            //Both Clone and Existing Object have Memory locations for the Address object
            emp2.EmpAddress.address = "Mumbai";

            Console.WriteLine("Emplpyee 1: ");
            Console.WriteLine("Name: " + emp1.Name + ", Address: " + emp1.EmpAddress.address + ", Dept: " + emp1.Department);
            Console.WriteLine("Emplpyee 2: ");
            Console.WriteLine("Name: " + emp2.Name + ", Address: " + emp2.EmpAddress.address + ", Dept: " + emp2.Department);

            Console.Read();
        }
    }

    public class Employee
    {
        //Value type Property
        public string Name { get; set; }
        //Value type Property
        public string Department { get; set; }
        //Reference type Property
        public Address EmpAddress { get; set; }

        //Creating a Cloned Object of the Current Object
        public Employee GetClone()
        {
            Employee employee = (Employee)this.MemberwiseClone();
            //The following Statement will make this a Deep Copy
            //Now, Cloned and Existing Object have different Memory Locations for the Address Object
            employee.EmpAddress = EmpAddress.GetClone();
            return employee;
        }
    }

    public class Address
    {
        public string address { get; set; }
        //Creating a Cloned Object of the Current Object
        public Address GetClone()
        {
            return (Address)this.MemberwiseClone();
        }
    }
}

// output 
Employee 1:
Name: Anurag, Address: BBSR, Dept: IT
Employee 2:
Name: Pranaya, Address: Mumbai, Dept: IT
```

- Note: In C#, a “deep copy” means creating a new object with a copy of the original object and all the objects inside it. In this case, changes to the new object won’t affect the original object or vice versa. To perform a deep copy, you typically have to manually copy each field of the object, taking care to also deep copy any reference types.