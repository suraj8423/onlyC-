using System;

namespace AbstractClass;

public abstract class Vehicle
{
    public abstract void Start();
    public abstract void Stop();
}

public class Car : Vehicle
{
    public override void Start()
    {
        Console.WriteLine("Car is starting.");
    }

    public override void Stop()
    {
        Console.WriteLine("Car is stopping.");
    }
}

public class ElectricTrain : Vehicle
{
    public override void Start()
    {
        Console.WriteLine("Electric train is starting.");
    }

    public override void Stop()
    {
        Console.WriteLine("Electric train is stopping.");
    }
}