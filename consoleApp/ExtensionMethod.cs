using System;
using GameZone;

namespace ExtensionMethod;

public static class GameZoneExtensions
{
    public static void DisplayLogOutMessage(this GameZoneClass gameZone)
    {
        Console.WriteLine("You have successfully logged out.");
        Console.WriteLine("Thank you for visiting the Game Zone!");
    }
}