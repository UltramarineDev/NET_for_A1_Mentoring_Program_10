using System;
using System.Configuration;

namespace BCL
{
    class Program
    {
        static FolderListenerService service;
        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ConsoleHandler);
            Console.WriteLine("(press 'ctrl+c' to stop).");
            DoWork();
        }

        private static void ConsoleHandler(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            service.Cancel();
        }

        private static void DoWork()
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var configSection = (StartupSettingsConfigSection)cfg.GetSection("StartupSettings");
            service = new FolderListenerService(configSection);
            service.Listen();
        }
    }
}
