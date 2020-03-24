using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BCL
{
    class Program
    {
        static void Main(string[] args)
        {
            //     < add key = "DefaultCulture" value = "en-EN" />

            //< add key = "FileNameTemplate" value = "[a-z0-9]+" />

            //  < add key = "DefaultDestinationFolder" value = "C:\TEST\Новая папка(2)" />

            //     < add key = "AddIndex" value = "true" />

            //       < add key = "AddMigrationDate" value = "true" />

            //Assembly mscorlib = Assembly.GetExecutingAssembly();
            //foreach (Type type in mscorlib.GetTypes())
            //{
            //    Console.WriteLine(type.FullName);
            //}

            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var configSection = (StartupSettingsConfigSection)cfg.GetSection("StartupSettings");
            //var service = new FolderListenerService(configSection);
            //service.Listen();
            var a = configSection.FilesItems[0].Name;
        }
    }
}
