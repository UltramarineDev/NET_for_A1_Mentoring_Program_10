using System.Collections.Generic;

namespace CSharpFundamentals
{
    public class SequencePrinter : IPrinter
    {
        private List<string> entries;

        public string[] entriesSequense { get { return entries.ToArray(); } }

        public SequencePrinter()
        {
            entries = new List<string>();
        }

        public void Print(string entry)
        {
            entries.Add(entry);
        }
    }
}
