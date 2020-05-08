using System;
using System.Collections.Generic;

namespace ExceptionHandling
{
    class Program
    {
        private static List<string> lines = new List<string>();
        private static bool isStop = false;

        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ConsoleHandler);
            Console.WriteLine("Enter line (press 'ctrl+c' to stop).");
            ReadLines();
        }

        private static void ReadLines()
        {
            do
            {
                var line = Console.ReadLine();
                if (line != null)
                {
                    lines.Add(line);
                }

            } while (!isStop);
        }

        private static void ConsoleHandler(object sender, ConsoleCancelEventArgs args)
        {
            isStop = true;
            PrintFirstCharacters();
            args.Cancel = true;
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
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Can not process empty line.");
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception occured during processing characters.");
                    continue;
                }
            }
        }
    }
}
