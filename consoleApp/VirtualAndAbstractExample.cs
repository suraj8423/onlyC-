using System;

namespace VirtualAndAbstractExample;

public abstract class Animal
{
    public string Name { get; set; }

    public abstract void MakeSound();

    public virtual void Eat()
    {
        Console.WriteLine($"{Name} is eating");
    }
}

public class Dog : Animal
{
    public Dog(string name)
    {
        Name = name;
    }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} barks");
    }
}

public class Cat : Animal
{
    public Cat(string name)
    {
        Name = name;
    }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} meows");
    }

    public override void Eat()
    {
        Console.WriteLine($"{Name} is eating fish");
    }
}