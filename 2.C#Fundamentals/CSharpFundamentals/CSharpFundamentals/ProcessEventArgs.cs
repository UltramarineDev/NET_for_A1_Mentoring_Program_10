using System;

namespace CSharpFundamentals
{
    public class ProcessEventArgs: EventArgs
    {
        private readonly string message;

        public ProcessEventArgs(string message)
        {
            this.message = message;
        }

        public string Message { get { return message; } }
    }
}
