using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebCrawler
{
    public class Crawler
    {
        private readonly AppRestrictions _appRestrictions;
        private readonly LocalRecorder _recorder;
        private HttpClient _client;
        private string baseUri;

        public Crawler(AppRestrictions appRestrictions, LocalRecorder recorder)
        {
            _appRestrictions = appRestrictions;
            _recorder = recorder;
            _client = new HttpClient();
        }

        public async Task DowloadSiteFromUrl(string url, string path)
        {
            baseUri = baseUri ?? url;

            var uri = new Uri(url);
            try
            {
                await ParseHtml(uri, path);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        private async Task ParseHtml(Uri uri, string path)
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            string responseBody = response.Content.ReadAsStringAsync().Result;
            response.EnsureSuccessStatusCode();

            var document = new HtmlDocument();
            document.LoadHtml(responseBody);
            _recorder.Record(path, uri, document);

            var links = document.DocumentNode.Descendants().SelectMany(d => d.Attributes.Where( atr => atr.Name == "href"));
            foreach (var link in links)
            {
                await DowloadSiteFromUrl(baseUri + link.Value, path);
            }
        }
    }
}
