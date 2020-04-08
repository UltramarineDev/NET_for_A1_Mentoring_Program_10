using System;

namespace CSharpFundamentals
{
    public class ProcessEventArgs: EventArgs
    {
        private readonly string message;
        private readonly bool stop;
        private readonly bool exclude;

        public ProcessEventArgs(string message, bool stop, bool exclude)
        {
            this.message = message;
            this.stop = stop;
            this.exclude = exclude;
        }

        public string Message { get { return message; } }
        public bool Stop { get { return stop; } }
        public bool Exclude { get { return exclude; } }
    }
}
