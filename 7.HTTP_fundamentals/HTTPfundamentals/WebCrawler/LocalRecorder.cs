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
        public void RecordHtml(string path, Uri uri, HtmlDocument document)
        {
            var pathToDirectory = GetPathToDirectory(path, uri);
            var name = document.DocumentNode.Descendants("title").FirstOrDefault().InnerText + ".html";//
            var filePath = Path.Combine(pathToDirectory, name);
            
            using (var memStream = new MemoryStream())
            {
                document.Save(memStream);
                Directory.CreateDirectory(pathToDirectory);
                var fileStream = File.Create(filePath);
                memStream.CopyTo(fileStream);
            }
        }

        public void RecordResouce(string path, Uri uri)
        {
            var filePath = GetPathToDirectory(path, uri);
            using (var memStream = new MemoryStream())
            {
                var fileStream = File.Create(filePath);
                memStream.CopyTo(fileStream);
            }
        }

        private string GetStringWithoutInvalidCharacters(string input)
            => Regex.Replace(input ?? string.Empty, "[<>:\"|?*]", string.Empty);

        private string GetPathToDirectory(string path, Uri uri)
            => string.Join(string.Empty, Path.Combine(path, uri.Host) + GetStringWithoutInvalidCharacters(uri.LocalPath).Replace("/", @"\"));
    }
}
