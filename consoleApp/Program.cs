using System;
using System.Runtime.CompilerServices;
using AbstractClass;
using InterfaceExample;
using InheritenceExample1;
using InheritenceExample2;
//using PolymorphismExample;
using VirtualAndAbstractExample;

class Program
{
    static void Main(string[] args)
    {
        Animal dog = new Dog("pappu");
        dog.MakeSound();
        dog.Eat();

        Animal cat = new Cat("moti");
        cat.MakeSound();
        cat.Eat();
    }
}