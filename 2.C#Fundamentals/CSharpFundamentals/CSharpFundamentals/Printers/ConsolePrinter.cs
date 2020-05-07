using System;

namespace CSharpFundamentals
{
    public class ConsolePrinter : IPrinter
    {
        public string[] entriesSequense => throw new NotImplementedException();

        public void Print(string entry)
        {
            Console.WriteLine(entry);
        }
    }
}
