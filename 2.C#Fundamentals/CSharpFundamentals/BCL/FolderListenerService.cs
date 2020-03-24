using System;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using NLog;
using messages = BCL.Resources.Messages;

namespace BCL
{
    public class FolderListenerService
    {
        private readonly DirectoryCollection directoryCollection;
        private readonly FileCollection fileCollection;
        private readonly StartupSettingsConfigSection configSection;
        private readonly Logger logger;

        public FolderListenerService(StartupSettingsConfigSection configSection)
        {
            //directories = ConfigurationManager.GetSection("listeningDirectories") as Directory[];
            this.configSection = configSection;
            directoryCollection = configSection.DirectoriesItems;
            fileCollection = configSection.FilesItems;

            logger = LogManager.GetCurrentClassLogger();

            CultureInfo culture = new CultureInfo(configSection.DefaultCulture);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        public void Listen()
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = directoryCollection[0].Path;//
                watcher.Created += OnChanged;//
                watcher.EnableRaisingEvents = true;
                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;//ctrl+c
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            this.logger.Info(messages.FileCreated);
            foreach (FileElement file in this.fileCollection)
            {
                Regex regex = new Regex(file.Name);
                if (regex.IsMatch(e.Name))
                {
                    logger.Info(messages.FileRuleFounded);
                    if (!File.Exists(e.FullPath))
                    {
                        File.Copy(e.FullPath, file.Path);
                        File.Delete(e.FullPath);
                        logger.Info(messages.FileMoved);
                        return;
                    }
                }
            }

            logger.Info(messages.FileRuleNotFounded);
            if (!File.Exists(e.FullPath))
            {
                File.Copy(e.FullPath, this.configSection.DefaultDestinationFolder);
                logger.Info(messages.FileMoved);
                File.Delete(e.FullPath);
            }
        }
    }
}
