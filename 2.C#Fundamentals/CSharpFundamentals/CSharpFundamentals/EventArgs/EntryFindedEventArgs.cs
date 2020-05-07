using System;

namespace CSharpFundamentals
{
    public class EntryFindedEventArgs : EventArgs
    {
        public EntryFindedEventArgs(string message, string entry)
        {
            Message = message;
            Entry = entry;
        }

        public string Message { get; }
        public string Entry { get; }

        public SearchAction SearchAction { get; set; }
    }
}
