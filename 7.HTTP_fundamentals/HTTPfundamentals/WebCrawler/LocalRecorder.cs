using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;


namespace WebCrawler
{
    public class LocalRecorder
    {
        public void RecordHtml(string path, Uri baseUri, Uri uri, HtmlDocument document)
        {
            var subUri = GetSubUri(baseUri, uri);
            var pathToDirectory = path + "\\info";

            if (!string.IsNullOrEmpty(subUri))
            {
                pathToDirectory = GetFilteredPath(path, uri);
            }
            var name = document.DocumentNode.Descendants("title").FirstOrDefault().InnerText + ".html";//
            var fileteredNme = GetStringWithoutInvalidCharacters(name);
            var filePath = Path.Combine(pathToDirectory, fileteredNme);

            using (var memStream = new MemoryStream())
            {
                document.Save(memStream);
                Directory.CreateDirectory(pathToDirectory);
                var fileStream = File.Create(filePath);
                memStream.CopyTo(fileStream);
            }
        }

        public void RecordResouce(string path, Uri baseUri, Uri uri)
        {
            var subUri = GetSubUri(baseUri, uri);
            var filePath = path + "\\info";

            if (!string.IsNullOrEmpty(subUri))
            {
                filePath = GetFilteredPath(path, uri);
            }

            var directoryInfo = new DirectoryInfo(path);
            // string filePath = Path.Combine(directoryInfo.FullName, uri.Host) + uri.LocalPath.Replace("/", @"\");
            var directoryPath = Path.GetDirectoryName(filePath);
            Directory.CreateDirectory(directoryPath);

            using (var memStream = new MemoryStream())
            {
                var fileStream = File.Create(filePath);
                memStream.CopyTo(fileStream);
            }
        }

        private string GetSubUri(Uri baseUri, Uri uri) 
            => new string(uri.AbsoluteUri.Except(baseUri.AbsoluteUri).ToArray<char>());

        private string GetStringWithoutInvalidCharacters(string input)
            => Regex.Replace(input ?? string.Empty, "https|[<>:;\"|?*]|//", string.Empty);

        private string GetFilteredPath(string path, Uri uri)
            => string.Join(string.Empty, Path.Combine(path, uri.Host) +
                GetStringWithoutInvalidCharacters(uri.LocalPath).Replace("/", @"\"));
    }
}
