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

            var operations = new NumberOperations();

            Console.WriteLine(operations.DigitsCount(123));
            Console.WriteLine(operations.Add(1, 2));
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
