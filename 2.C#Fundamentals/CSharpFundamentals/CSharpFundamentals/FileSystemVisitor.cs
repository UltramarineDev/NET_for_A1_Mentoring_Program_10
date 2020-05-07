using System;
using System.Collections.Generic;
using System.IO;

namespace CSharpFundamentals
{
    public class FileSystemVisitor
    {
        public event EventHandler<ProcessEventArgs> Start;
        public event EventHandler<ProcessEventArgs> Finish;
        public event EventHandler<EntryFindedEventArgs> FileFinded;
        public event EventHandler<EntryFindedEventArgs> DirectoryFinded;
        public event EventHandler<EntryFindedEventArgs> FilteredFileFinded;
        public event EventHandler<EntryFindedEventArgs> FilteredDirectoryFinded;

        private readonly Func<string, bool> _predicate;

        private const string StartMessage = "Start searching";
        private const string FinishMessage = "Finish searching";
        private const string FileFindedMessage = "File finded";
        private const string DirectoryFindedMessage = "Directory finded";
        private const string FilteredFileFindedMessage = "Filtered file finded";
        private const string FilteredDirectoryFindedMessage = "Filtered directory finded";

        public FileSystemVisitor() : this(null) { }

        public FileSystemVisitor(Func<string, bool> predicate)
        {
            _predicate = predicate;
        }

        public IEnumerable<string> VisitFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                yield break;
            }

            Publish(Start, new ProcessEventArgs(StartMessage));

            foreach (var entry in GenerateFileSystemEntries(path, SearchAction.Continue))
            {
                var action = SearchAction.Continue;

                if (File.GetAttributes(entry) == FileAttributes.Directory)
                {
                    action = HandleFindedEntry(DirectoryFinded, entry, DirectoryFindedMessage);
                }
                else
                {
                    action = HandleFindedEntry(FileFinded, entry, FileFindedMessage);
                }

                if (action == SearchAction.Stop)
                {
                    yield break;
                }

                if (action == SearchAction.Exclude)
                {
                    continue;
                }

                yield return entry;

                var filtered = TryFilter(entry, out var filteredEntry);
                if (filtered)
                {
                    if (File.GetAttributes(filteredEntry) == FileAttributes.Directory)
                    {
                        action = HandleFindedEntry(FilteredDirectoryFinded, entry, FilteredDirectoryFindedMessage);
                    }
                    else
                    {
                        action = HandleFindedEntry(FilteredFileFinded, entry, FilteredFileFindedMessage);
                    }

                    yield return entry;
                }
            }

            Publish(Finish, new ProcessEventArgs(FinishMessage));
        }

        private IEnumerable<string> GenerateFileSystemEntries(string path, SearchAction searchAction)
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
                    foreach (var directoryEntry in GenerateFileSystemEntries(entry, searchAction))
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

        private void Publish<TEventArgs>(EventHandler<TEventArgs> @event, TEventArgs e)
        {
            @event?.Invoke(this, e);
        }

        private SearchAction HandleFindedEntry(EventHandler<EntryFindedEventArgs> @event, string entry, string message)
        {
            var entryFindedEventArgs = new EntryFindedEventArgs(message, entry)
            {
                SearchAction = SearchAction.Continue
            };

            Publish(@event, entryFindedEventArgs);

            return entryFindedEventArgs.SearchAction;
        }
    }
}
