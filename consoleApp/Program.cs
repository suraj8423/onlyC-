using System;
using ExtensionMethod;
using GameZone;

namespace consoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GameZoneClass gameZone = new GameZoneClass();
            gameZone.Display();

            // Simulate user logging out
            Console.WriteLine("User is logging out...");
            gameZone.DisplayLogOutMessage();
        }
    }
}