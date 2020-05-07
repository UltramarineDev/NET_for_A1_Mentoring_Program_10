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

            var fileSystemVisitor = new FileSystemVisitor();
            IPrinter printer = new ConsolePrinter();
            fileSystemVisitor.Start += DisplayMessage;
            fileSystemVisitor.Finish += DisplayMessage;
            fileSystemVisitor.FileFinded += DisplayMessageWithActions;
            fileSystemVisitor.DirectoryFinded += DisplayMessageWithActions;
            fileSystemVisitor.FilteredFileFinded += DisplayMessageWithActions;
            fileSystemVisitor.FilteredDirectoryFinded += DisplayMessageWithActions;

            var entryForStop = "C:\\TEST\\Новая папка (2)";

            foreach (var entry in fileSystemVisitor.VisitFolder(path))
            {
                printer.Print(entry);
            }

            Console.WriteLine("Enter pattern:");
            pattern = Console.ReadLine();
            var predicateGenerator = new PredicateGenerator(pattern);
            Func<string, bool> predicate = predicateGenerator.GetPredicate;

            var fileSystemVisitorWithPattern = new FileSystemVisitor(predicate);

            fileSystemVisitorWithPattern.Start += DisplayMessage;
            fileSystemVisitorWithPattern.Finish += DisplayMessage;
            fileSystemVisitorWithPattern.FileFinded += DisplayMessageWithActions;
            fileSystemVisitorWithPattern.DirectoryFinded += DisplayMessageWithActions;
            fileSystemVisitorWithPattern.FilteredFileFinded += DisplayMessageWithActions;
            fileSystemVisitorWithPattern.FilteredDirectoryFinded += DisplayMessageWithActions;

            foreach (var entry in fileSystemVisitorWithPattern.VisitFolder(path))
            {
                printer.Print(entry);
            }
        }

        private static void DisplayMessage(object sender, ProcessEventArgs e)
        {
            Console.WriteLine("Program received: {0}", e.Message);
        }

        private static void DisplayMessageWithActions(object sender, EntryFindedEventArgs e)
        {
            var foldersToExclude = new List<string> { "C:\\TEST\\BCL" };
            var pathToBreak = "C:\\TEST\\XLSX Worksheet.xlsx";

            Console.WriteLine("Program received: {0}", e.Message);

            if (foldersToExclude.Contains(e.Entry))
            {
                e.SearchAction = SearchAction.Exclude;
            }

            if (e.Entry == pathToBreak)
            {
                e.SearchAction = SearchAction.Stop;
            }
        }
    }
}
