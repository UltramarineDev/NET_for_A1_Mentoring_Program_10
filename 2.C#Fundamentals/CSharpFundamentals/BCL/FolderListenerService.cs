using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using messages = BCL.Resources.Messages;

namespace BCL
{
    public class FolderListenerService
    {
        private readonly DirectoryCollection _directoryCollection;
        private readonly RulesCollection _rulesCollection;
        private readonly StartupSettingsConfigSection _configSection;
        private readonly CultureInfo _culture;

        private static bool isStop = false;

        public FolderListenerService(StartupSettingsConfigSection configSection)
        {
            _configSection = configSection;
            _directoryCollection = _configSection.DirectoriesItems;
            _rulesCollection = _configSection.RulesItems;
            _culture = _configSection.Culture;
            CultureInfo.DefaultThreadCurrentCulture = _culture;
            CultureInfo.DefaultThreadCurrentUICulture = _culture;
        }

        public void Listen()
        {
            var watchers = new List<FileSystemWatcher>();
            foreach (DirectoryElement dir in _directoryCollection)
            {
                var watcher = new FileSystemWatcher
                {
                    Path = dir.Path,
                    IncludeSubdirectories = false
                };

                watcher.EnableRaisingEvents = true;
                watcher.Created += OnChanged;
                while (!isStop);
            }
        }

        public void Cancel()
        {
            isStop = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            var attributes = File.GetAttributes(e.FullPath);
            if (attributes.HasFlag(FileAttributes.Directory))
            {
                return;
            }

            Logger.Log(messages.FileCreated);
            foreach (RuleElement file in _rulesCollection)
            {
                Regex regex = new Regex(file.FileName);
                if (regex.IsMatch(e.Name))
                {
                    Logger.Log(messages.FileRuleFounded);
                    if (!File.Exists(file.Path))
                    {
                        var newPath = Path.Combine(file.Path, e.FullPath.Split('\\').Last());
                        File.Copy(e.FullPath, newPath);
                        File.Delete(e.FullPath);
                        Logger.Log(messages.FileMoved);
                        return;
                    }
                }
            }
        }
    }
}
