```csharp
using System.Collections.Generic;
namespace LINQDemo
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
        public List<string> Technology { get; set; }

        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee {ID = 101, Name = "Preety", Gender = "Female", Salary = 60000,
                              Technology = new List<string>() {"C#", "Jave", "C++"} },
                new Employee {ID = 102, Name = "Priyanka", Gender = "Female", Salary = 50000,
                              Technology =new List<string>() { "WCF", "SQL Server", "C#" } },
                new Employee {ID = 103, Name = "Hina", Gender = "Female", Salary = 40000,
                              Technology =new List<string>() { "MVC", "Jave", "LINQ"}},
                new Employee {ID = 104, Name = "Anurag", Gender = "Male", Salary = 450000},
                new Employee {ID = 105, Name = "Sambit", Gender = "Male", Salary = 550000},
                new Employee {ID = 106, Name = "Sushanta", Gender = "Male", Salary = 700000,
                             Technology =new List<string>() { "ADO.NET", "C#", "LINQ" }}

            };

            return employees;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Query Syntax
            var QuerySyntax = from employee in Employee.GetEmployees()
                              where employee.Salary > 50000
                              select employee;
            //Method Syntax
            var MethodSyntax = Employee.GetEmployees()
                               .Where(emp => emp.Salary > 50000);
            
            foreach (var emp in QuerySyntax)
            {
                Console.WriteLine($"Name : {emp.Name}, Salary : {emp.Salary}, Gender : {emp.Gender}");
                if(emp.Technology != null && emp.Technology.Count() > 0)
                {
                    Console.Write(" Technology : ");
                    foreach (var tech in emp.Technology)
                    {
                        Console.Write(tech + " ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine(" Technology Not Available ");
                }
            }

            Console.ReadKey();
        }
    }
}
```