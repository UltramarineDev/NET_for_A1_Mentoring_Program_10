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
            var appRestrictions = new AppRestrictions(2, DomainTransitionOptions.Unlimited, new string[] { "jpj", "gif", "pdf", "jpeg", "png" }, true);
            var recorder = new LocalRecorder();
            var logger = new ConsoleLogger();
            var crawler = new Crawler(appRestrictions, recorder, logger);
            string url = "http://www.contoso.com/";
            string url2 = "https://unsplash.com/";
            string url3 = "https://m.habr.com/img/marathon.png";
            string url4 = "https://m.habr.com/ru/post/424873/";
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            var assemblyDirectoryPath = Path.GetDirectoryName(path);

            await crawler.DowloadSiteFromUrl(url3, assemblyDirectoryPath);
        }
    }
}
