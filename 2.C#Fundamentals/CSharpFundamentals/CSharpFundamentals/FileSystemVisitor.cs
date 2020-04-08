using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpFundamentals
{
    public class FileSystemVisitor
    {
        private readonly Func<string, bool> _predicate;
        private readonly EventNotifier _eventNotifier;

        private const string StartMessage = "Start searching";
        private const string FinishMessage = "Finish searching";
        private const string FileFinded = "File finded";
        private const string DirectoryFinded = "Directory finded";
        private const string FilteredFileFinded = "Filtered file finded";
        private const string FilteredDirectoryFinded = "Filtered directory finded";

        public bool isStop;
        public bool isExclude;

        public FileSystemVisitor(EventNotifier eventNotifier) : this(null, eventNotifier) { }

        public FileSystemVisitor(Func<string, bool> predicate, EventNotifier eventNotifier)
        {
            _predicate = predicate;
            _eventNotifier = eventNotifier;
            isStop = false;
            isExclude = false;
        }

        public IEnumerable<string> VisitFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                yield break;
            }

            _eventNotifier.Publish(StartMessage, isStop);

            foreach (var entry in GenerateFileSystemEntries(path))
            {
                if (isStop)
                {
                    _eventNotifier.Publish(StartMessage, isStop);
                    yield break;
                }

                if (isExclude)
                {
                    _eventNotifier.Publish(StartMessage, isStop, isExclude);
                    isExclude = false;
                    continue;
                }

                GenerateEvent(entry, DirectoryFinded, FileFinded);
                yield return entry;

                var filtered = TryFilter(entry, out var filteredEntry);
                if (filtered)
                {
                    GenerateEvent(filteredEntry, FilteredDirectoryFinded, FilteredFileFinded);
                    yield return entry;
                }
            }

            _eventNotifier.Publish(FinishMessage, isStop);
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
