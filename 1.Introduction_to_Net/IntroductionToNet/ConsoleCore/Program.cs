using System;

namespace IntroductionToNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter user name:");
            var userName = Console.ReadLine();
            Console.WriteLine($"Hello, {userName}!");
        }
    }
}