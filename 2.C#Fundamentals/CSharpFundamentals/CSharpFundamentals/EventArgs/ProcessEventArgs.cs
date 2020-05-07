using System;

namespace CSharpFundamentals
{
    public class ProcessEventArgs: EventArgs 
    {
        private readonly string _message;
        public ProcessEventArgs(string message)
        {
            _message = message;
        }

        public string Message { get { return _message; } }
    }
}
