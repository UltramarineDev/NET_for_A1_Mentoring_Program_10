using System;
using ClassLibrary;

namespace IntroductionToNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter user name:");
            var userName = Console.ReadLine();
            Console.WriteLine(Class1.GetGreeting(userName));
        }
    }
}