using System;

namespace WebCrawler
{
    public class ConsoleLogger
    {
        internal bool Verbose { get; set; }

        public void Log(string message)
        {
            if (Verbose)
            {
                Console.WriteLine(message);
            }
        }
    }
}
