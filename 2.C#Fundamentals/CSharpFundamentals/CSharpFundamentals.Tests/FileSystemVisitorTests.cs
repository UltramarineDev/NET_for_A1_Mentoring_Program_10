using System;
using NUnit.Framework;

namespace CSharpFundamentals.Tests
{
    public class FileSystemVisitorTests
    {
        [Test]
        public void GetAllFilesAndFolders_PredicateIsNull_OK()
        {
            const string path = "C:\\TEST";

            EventNotifier eventNotifier = new EventNotifier();
            IPrinter printer = new SequencePrinter();
            var fileSystemVisitor = new FileSystemVisitor(eventNotifier, printer);
            fileSystemVisitor.GetAllFilesAndFolders(path);

            string[] expected = {
                "C:\\TEST\\WPS Writer Document.wps",
                "C:\\TEST\\XLSX Worksheet.xlsx",
                "C:\\TEST\\Новая папка",
                "C:\\TEST\\Новая папка (2)",
                "C:\\TEST\\Новая папка\\XLSX Worksheet.xlsx",
                "C:\\TEST\\Новая папка (2)\\BCL",
                "C:\\TEST\\Новая папка (2)\\Новая папка",
                "C:\\TEST\\Новая папка (2)\\Новый текстовый документ.txt",
                "C:\\TEST\\Новая папка (2)\\BCL\\NewFolder",
            };

            string[] actual = printer.entriesSequense;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAllFilesAndFolders_PredicateExists_OK()
        {
            const string path = "C:\\TEST";
            const string pattern = "Нова";
            var predicateGenerator = new PredicateGenerator(pattern);
            Func<string, bool> predicate = predicateGenerator.GetPredicate;
            EventNotifier eventNotifier = new EventNotifier();
            IPrinter printer = new SequencePrinter();
            var fileSystemVisitor = new FileSystemVisitor(predicate, eventNotifier, printer);
            fileSystemVisitor.GetAllFilesAndFolders(path);

            string[] expected = {
                "C:\\TEST\\WPS Writer Document.wps",
                "C:\\TEST\\XLSX Worksheet.xlsx",
                "C:\\TEST\\Новая папка",
                "C:\\TEST\\Новая папка",
                "C:\\TEST\\Новая папка (2)",
                "C:\\TEST\\Новая папка (2)",
                "C:\\TEST\\Новая папка\\XLSX Worksheet.xlsx",
                "C:\\TEST\\Новая папка (2)\\BCL",
                "C:\\TEST\\Новая папка (2)\\Новая папка",
                "C:\\TEST\\Новая папка (2)\\Новая папка",
                "C:\\TEST\\Новая папка (2)\\Новый текстовый документ.txt",
                "C:\\TEST\\Новая папка (2)\\BCL\\NewFolder",
            };

            string[] actual = printer.entriesSequense;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAllFilesAndFolders_DirectoryDoesNotExists_Return()
        {
            var path = "Invalid\\path";
            EventNotifier eventNotifier = new EventNotifier();
            IPrinter printer = new SequencePrinter();
            var fileSystemVisitor = new FileSystemVisitor(eventNotifier, printer);
            fileSystemVisitor.GetAllFilesAndFolders(path);

            string[] expected = { };
            string[] actual = printer.entriesSequense;

            Assert.AreEqual(expected, actual);
        }
    }
}
