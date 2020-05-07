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
            IPrinter printer = new SequencePrinter();

            string[] expected = {
                "C:\\TEST\\BCL",
                "C:\\TEST\\WPS Writer Document.wps",
                "C:\\TEST\\XLSX Worksheet.xlsx",
                "C:\\TEST\\Новая папка",
                "C:\\TEST\\Новая папка (2)",
                "C:\\TEST\\BCL\\NewFolder",
                "C:\\TEST\\Новая папка\\XLSX Worksheet.xlsx",
                "C:\\TEST\\Новая папка (2)\\BCL",
                "C:\\TEST\\Новая папка (2)\\Новая папка",
                "C:\\TEST\\Новая папка (2)\\Новый текстовый документ.txt",
                "C:\\TEST\\Новая папка (2)\\BCL\\NewFolder",
            };

            var fileSystemVisitor = new FileSystemVisitor();
            foreach (var entry in fileSystemVisitor.VisitFolder(path))
            {
                printer.Print(entry);
            }

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
            IPrinter printer = new SequencePrinter();

            string[] expected = {
                "C:\\TEST\\BCL",
                "C:\\TEST\\WPS Writer Document.wps",
                "C:\\TEST\\XLSX Worksheet.xlsx",
                "C:\\TEST\\Новая папка",
                "C:\\TEST\\Новая папка",
                "C:\\TEST\\Новая папка (2)",
                "C:\\TEST\\Новая папка (2)",
                "C:\\TEST\\BCL\\NewFolder",
                "C:\\TEST\\Новая папка\\XLSX Worksheet.xlsx",
                "C:\\TEST\\Новая папка (2)\\BCL",
                "C:\\TEST\\Новая папка (2)\\Новая папка",
                "C:\\TEST\\Новая папка (2)\\Новая папка",
                "C:\\TEST\\Новая папка (2)\\Новый текстовый документ.txt",
                "C:\\TEST\\Новая папка (2)\\BCL\\NewFolder",
            };

            var fileSystemVisitor = new FileSystemVisitor(predicate);
            foreach (var entry in fileSystemVisitor.VisitFolder(path))
            {
                printer.Print(entry);
            }

            string[] actual = printer.entriesSequense;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAllFilesAndFolders_DirectoryDoesNotExists_Return()
        {
            var path = "Invalid\\path";
            IPrinter printer = new SequencePrinter();
            var fileSystemVisitor = new FileSystemVisitor();
            fileSystemVisitor.VisitFolder(path);

            string[] expected = { };
            string[] actual = printer.entriesSequense;

            Assert.AreEqual(expected, actual);
        }
    }
}
