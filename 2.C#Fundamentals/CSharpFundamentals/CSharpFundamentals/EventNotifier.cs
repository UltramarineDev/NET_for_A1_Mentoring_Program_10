using System;

namespace CSharpFundamentals
{
    public class EventNotifier
    {
        public event EventHandler<ProcessEventArgs> Process = delegate { };

        public void Publish(string messageToPublish)
        {
            Begin(this, new ProcessEventArgs(messageToPublish));
        }

        private void Begin(object sender, ProcessEventArgs e)
        {
            Process?.Invoke(this, e);
        }
    }
}
