using System;
using System.Collections.Generic;

namespace CSharpFundamentals
{
    class Program
    {
        private static string pattern;
        static void Main()
        {
            Console.WriteLine("Enter path:");
            var path = Console.ReadLine();

            EventNotifier eventNotifier = new EventNotifier();
            eventNotifier.Process += DisplayMessage;

            var fileSystemVisitor = new FileSystemVisitor(eventNotifier);
            IPrinter printer = new ConsolePrinter();

            var entryForStop = "C:\\TEST\\Новая папка (2)";
            var foldersToExclude = new List<string> { "C:\\TEST\\XLSX Worksheet.xlsx", "C:\\TEST\\Новая папка" };

            foreach (var entry in fileSystemVisitor.VisitFolder(path))
            {
                if (entry == entryForStop)
                {
                    fileSystemVisitor.isStop = true;
                }

                if (foldersToExclude.Contains(entry))
                {
                    fileSystemVisitor.isExclude = true;
                    continue;
                }

                printer.Print(entry);
            }


            Console.WriteLine("Enter pattern:");
            pattern = Console.ReadLine();
            var predicateGenerator = new PredicateGenerator(pattern);
            Func<string, bool> predicate = predicateGenerator.GetPredicate;

            var fileSystemVisitorWithPattern = new FileSystemVisitor(predicate, eventNotifier);
            foreach (var entry in fileSystemVisitorWithPattern.VisitFolder(path))
            {
                printer.Print(entry);
            }
        }

        private static void DisplayMessage(object sender, ProcessEventArgs e)
        {
            Console.WriteLine("Program received: {0}", e.Message);
            if (e.Stop)
            {
                Console.WriteLine("Stop searching...");
            }

            if (e.Exclude)
            {
                Console.WriteLine("File/folder excluded.");
            }
        }
    }
}
