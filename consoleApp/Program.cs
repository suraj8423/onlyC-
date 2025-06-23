using System;
namespace ReadOnlyDemo
{
    class Program
    {
        readonly int number = 5;

        //You can also initialize through constructor
        public Program()
        {
            number = 20;
        }
        
        static void Main(string[] args)
        {
            Program obj = new Program();
            Console.WriteLine(obj.number);

            //You cannot change the value of a readonly variable once it is initialized
            //The following statement will give us compile time error 
            //obj.number = 20;

            Console.ReadLine();
        }
    }
}