using System;

namespace InheritenceExample1;

public class Vehicle
{
    public int Speed { get; protected set; }

    public void Start()
    {
        Console.WriteLine("Vehicle is starting.");
    }

    public void Stop()
    {
        Console.WriteLine("Vehicle is stopping.");
    }

    public virtual void Accelerate()
    {
        Speed += 5;
        Console.WriteLine($"Vehicle is accelerating. Current speed: {Speed} km/h");
    }
}

public class Car : Vehicle
{
    public int Doors { get; set; }

    public override void Accelerate()
    {
        Speed += 10;
        Console.WriteLine($"Car is accelerating. Current speed: {Speed} km/h");
    }

    public void OpenSunroof()
    {
        Console.WriteLine("Opening sunroof.");
    }
}

public class Motorcycle : Vehicle
{
    public bool HasSidecar { get; set; }

    public override void Accelerate()
    {
        Speed += 7;
        Console.WriteLine($"Motorcycle is accelerating. Current speed: {Speed} km/h");
    }

    public void UseKickStand()
    {
        Console.WriteLine("Kickstand placed!");
    }
}