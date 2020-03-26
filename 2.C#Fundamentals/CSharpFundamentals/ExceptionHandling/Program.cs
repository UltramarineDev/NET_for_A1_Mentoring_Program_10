using System;
using System.Collections.Generic;

namespace ExceptionHandling
{
    class Program
    {
        private static List<string> lines = new List<string>();
        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ConsoleHandler);
            Console.WriteLine("Enter line (press 'ctrl+c' to stop).");

            do
            {
                var line = Console.ReadLine();
                lines.Add(line);
            } while (true);
        }

        private static void ConsoleHandler(object sender, ConsoleCancelEventArgs args)
        {
            PrintFirstCharacters();
            return;
        }

        private static void PrintFirstCharacters()
        {
            Console.WriteLine("First characters from each line:");

            foreach (var line in lines)
            {
                try
                {
                    Console.WriteLine(line[0]);
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }
    }
}
