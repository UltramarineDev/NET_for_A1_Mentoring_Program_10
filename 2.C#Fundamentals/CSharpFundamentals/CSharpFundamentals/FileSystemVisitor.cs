using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpFundamentals
{
    public class FileSystemVisitor
    {
        private readonly Func<string, bool> _predicate;
        private readonly EventNotifier _eventNotifier;
        private readonly IPrinter _printer;

        private const string StartMessage = "Start searching";
        private const string FinishMessage = "Finish searching";
        private const string FileFinded = "File finded";
        private const string DirectoryFinded = "Directory finded";
        private const string FilteredFileFinded = "Filtered file finded";
        private const string FilteredDirectoryFinded = "Filtered directory finded";

        public FileSystemVisitor(EventNotifier eventNotifier, IPrinter printer) : this(null, eventNotifier, printer) { }

        public FileSystemVisitor(Func<string, bool> predicate, EventNotifier eventNotifier, IPrinter printer)
        {
            _predicate = predicate;
            _eventNotifier = eventNotifier;
            _printer = printer;
        }

        public void GetAllFilesAndFolders(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Invalid path");
                return;
            }

            _eventNotifier.Publish(StartMessage);

            foreach (var entry in GenerateFileSystemEntries(path))
            {
                GenerateEvent(entry, DirectoryFinded, FileFinded);
                _printer.Print(entry);

                var filtered = TryFilter(entry, out var filteredEntry);
                if (filtered)
                {
                    GenerateEvent(filteredEntry, FilteredDirectoryFinded, FilteredFileFinded);
                    _printer.Print(filteredEntry);
                }
            }

            _eventNotifier.Publish(FinishMessage);
        }

        private IEnumerable<string> GenerateFileSystemEntries(string path)
        {
            var entries = Directory.GetFileSystemEntries(path);

            foreach (var entry in entries)
            {
                yield return entry;
            }

            foreach (var entry in entries)
            {
                if (Directory.Exists(entry))
                {
                    foreach (var directoryEntry in GenerateFileSystemEntries(entry))
                    {
                        yield return directoryEntry;
                    }
                }
            }
        }

        private bool TryFilter(string entry, out string filteredEntry)
        {
            filteredEntry = null;

            if (_predicate != null && _predicate(entry))
            {
                filteredEntry = entry;
                return true;
            }

            return false;
        }

        private void GenerateEvent(string entry, string directoryMessage, string fileMessage)
        {
            if (File.GetAttributes(entry) == FileAttributes.Directory)
            {
                _eventNotifier.Publish(directoryMessage);
            }
            else
            {
                _eventNotifier.Publish(fileMessage);
            }
        }
    }
}
