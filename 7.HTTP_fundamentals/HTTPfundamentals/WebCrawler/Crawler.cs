using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebCrawler
{
    public class Crawler
    {
        private readonly AppRestrictions _appRestrictions;
        private readonly LocalRecorder _recorder;
        private readonly ConsoleLogger _logger;
        private HttpClient _client;
        private Uri _baseUri;
        private List<Uri> _visitedUri;

        public Crawler(AppRestrictions appRestrictions, LocalRecorder recorder, ConsoleLogger logger)
        {
            _appRestrictions = appRestrictions;
            _recorder = recorder;
            _client = new HttpClient();
            _visitedUri = new List<Uri>();
            _logger = logger;
            _logger.Verbose = _appRestrictions.Verbose;
        }

        public async Task DowloadSiteFromUrl(string url, string path)
        {
            ValidateRestrictions();
            _baseUri = new Uri(url);

            await ProcessUri(_baseUri, path);
        }

        private async Task ProcessUri(Uri uri, string path, int depthLevel = 0)
        {
            _logger.Log("Start uri processng...");
            _logger.Log($"Got base uri {uri}, root path: {path}, depth level: {depthLevel}");

            if (depthLevel > _appRestrictions.LinkAnalysisDepth || _visitedUri.Contains(uri) || !SatisfyDomainTransitionRestriction(uri))
            {
                _logger.Log("Finish uri processing");
                return;
            }

            var headResponseMessage = _client.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri)).Result;
            if (!headResponseMessage.IsSuccessStatusCode) return;

            try
            {
                if (headResponseMessage.Content.Headers.ContentType.MediaType == "text/html")
                {
                    _logger.Log($"Html found. {uri}");
                    await ParseHtml(uri, path, depthLevel);
                }
                else
                {
                    _logger.Log($"Resource found. {uri}");
                    await LoadResource(uri, path);
                }
            }
            catch (Exception e)
            {
                _logger.Log($"Exception occured while processing uri: {e.Message}");
            }
        }

        private async Task ParseHtml(Uri uri, string path, int depthLevel)
        {
            var response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            _visitedUri.Add(uri);

            var document = new HtmlDocument();
            document.LoadHtml(responseBody);

            var createdPath = _recorder.RecordHtml(path, _baseUri, uri, document);

            var links = document.DocumentNode.Descendants().SelectMany(d => d.Attributes.Where(atr => atr.Name == "href" || atr.Name == "src"));
            foreach (var link in links)
            {
                await ProcessUri(new Uri(link.Value), createdPath, depthLevel + 1);
            }
        }

        private void ValidateRestrictions()
        {
            if (_appRestrictions.LinkAnalysisDepth < 0)
            {
                throw new ArgumentException(nameof(_appRestrictions.LinkAnalysisDepth), "Link analysis depth can not be less than zero.");
            }
        }

        private async Task LoadResource(Uri uri, string path)
        {
            var satistyExtensionsRestriction = _appRestrictions.DowloadResourceExtensions.Any(ext => uri.Segments.Last().EndsWith(ext));
            if (!satistyExtensionsRestriction)
            {
                return;
            }

            var response = await _client.GetAsync(uri);
            string responseBody = response.Content.ReadAsStringAsync().Result;
            response.EnsureSuccessStatusCode();

            _recorder.RecordResouce(path, _baseUri, uri);
        }

        private bool SatisfyDomainTransitionRestriction(Uri uri)
        {
            switch (_appRestrictions.DomainTransitionOption)
            {
                case DomainTransitionOptions.Unlimited:
                    return true;
                case DomainTransitionOptions.WithinCurrentDomain:
                    return uri.Host == _baseUri.Host;
                case DomainTransitionOptions.WithinCurrentUrl:
                    return _baseUri.IsBaseOf(uri);
                default: return false;
            }
        }
    }
}
