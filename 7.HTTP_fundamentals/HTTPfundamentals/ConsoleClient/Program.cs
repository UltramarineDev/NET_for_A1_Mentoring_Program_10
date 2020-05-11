using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCrawler;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var appRestrictions = new AppRestrictions(0, DomainTransitionOptions.Unlimited, new string[] { "jpj", "gif", "pdf"}, false);
            var recorder = new LocalRecorder();
            var crawler = new Crawler(appRestrictions, recorder);
            string url = "http://www.contoso.com/";
            string path = "C:\\NET_for_A1_Mentoring_Program_10\\7.HTTP_fundamentals";

            try
            {
                await crawler.DowloadSiteFromUrl(url, path);
            }
            catch
            {

            }
        }
    }
}
