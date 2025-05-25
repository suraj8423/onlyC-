using System;

namespace SealedDemo;

public class Printer
{
    public virtual void Display()
    {
        Console.WriteLine("Display Dimension : 5*5");
    }
    public virtual void Print()
    {
        Console.WriteLine("Printer is printing....\n");
    }
}

public class LaserJet : Printer
{
    public sealed override void Display()
    {
        Console.WriteLine("Display Dimension : 10*10");
    }

    public override void Print()
    {
        Console.WriteLine("LaserJet Printer is printing....\n");
    }
}

public sealed class InkJet : LaserJet
{
    public override void Print()
    {
        Console.WriteLine("InkJet Printer is printing....\n");
    }

    // The following method cannot be overridden because InkJet is sealed
    // public override void Display()
    // {
    //     Console.WriteLine("Display Dimension : 15*15");
    // }
}