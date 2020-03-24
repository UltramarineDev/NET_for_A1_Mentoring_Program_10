using System;

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
            IPrinter printer = new ConsolePrinter();
            eventNotifier.Process += DisplayMessage;

            var fileSystemVisitor = new FileSystemVisitor(eventNotifier, printer);
            fileSystemVisitor.GetAllFilesAndFolders(path);
            
            Console.WriteLine("Enter pattern:");
            pattern = Console.ReadLine();
            var predicateGenerator = new PredicateGenerator(pattern);
            Func<string, bool> predicate = predicateGenerator.GetPredicate;
       
            var fileSystemVisitorWithPattern = new FileSystemVisitor(predicate, eventNotifier, printer);
            fileSystemVisitorWithPattern.GetAllFilesAndFolders(path);
        }

        private static void DisplayMessage(object sender, ProcessEventArgs e)
        {
            Console.WriteLine("Program received: {0}", e.Message);
        }
    }
}
