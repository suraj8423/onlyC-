using System;
namespace PartialClassDemo;

    class Program
    {
        static void Main(string[] args)
        {
            PartialEmployee emp = new()
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
