using System;
using LibraryNuget;

namespace ConsoleAppStyleCop
{
    public class Program
    {
        public static string Name { get; set; }
        public static int Idwrong { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(NumberOperations.DigitsCount(123));
        }

        private class ChildClass
        {
            public ChildClass()
            {
                Console.WriteLine("ChildClass created.");
            }
        }
    }
}
