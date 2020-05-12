using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using WebCrawler;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var appRestrictions = new AppRestrictions(0, DomainTransitionOptions.Unlimited, new string[] { "jpj", "gif", "pdf" }, true);
            var recorder = new LocalRecorder();
            var logger = new ConsoleLogger();
            var crawler = new Crawler(appRestrictions, recorder, logger);
            string url = "http://www.contoso.com/";

            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            var assemblyDirectoryPath = Path.GetDirectoryName(path);

            await crawler.DowloadSiteFromUrl(url, assemblyDirectoryPath);
        }
    }
}
