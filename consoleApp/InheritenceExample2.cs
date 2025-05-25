using System;

namespace InheritenceExample2;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }

    public Person(string name, int age, string address)
    {
        Name = name;
        Age = age;
        Address = address;
    }
    public void DisplayDetails()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}, Address: {Address}");
    }
}

public class Teacher : Person
{
    public int EmplyoeeId { get; set; }

    public Teacher(string name, int age, string address, int emplyoeeId) : base(name, age, address)
    {
        EmplyoeeId = emplyoeeId;
    }

    public void Teach(string subject)
    {
        Console.WriteLine($"{Name} is teaching {subject}.");
    }
}