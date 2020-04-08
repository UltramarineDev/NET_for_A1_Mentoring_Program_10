using System;

namespace CSharpFundamentals
{
    public class EventNotifier
    {
        public event EventHandler<ProcessEventArgs> Process = delegate { };

        public void Publish(string messageToPublish, bool stop = false, bool exclude = false)
        {
            Begin(this, new ProcessEventArgs(messageToPublish, stop, exclude));
        }

        private void Begin(object sender, ProcessEventArgs e)
        {
            Process?.Invoke(this, e);
        }
    }
}
