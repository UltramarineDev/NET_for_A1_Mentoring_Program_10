using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;


namespace WebCrawler
{
    public class LocalRecorder
    {
        public string RecordHtml(string path, Uri baseUri, Uri uri, HtmlDocument document)
        {
            var subUri = GetSubUri(baseUri, uri);
            var pathToDirectory = path + "\\" + GetStringWithoutInvalidCharacters(baseUri.AbsoluteUri);

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

                memStream.Seek(0, SeekOrigin.Begin);
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    memStream.CopyTo(fs);
                    fs.Flush();
                }
            }

            return pathToDirectory;
        }

        public void RecordResouce(string path, Uri baseUri, Uri uri)
        {
            var subUri = GetSubUri(baseUri, uri);
            var filePath = path + "\\" + GetStringWithoutInvalidCharacters(baseUri.AbsoluteUri);

            if (!string.IsNullOrEmpty(subUri))
            {
                filePath = GetFilteredPath(path, uri);
            }

            var directoryPath = Path.GetDirectoryName(filePath);
            Directory.CreateDirectory(directoryPath);

            using (var memStream = new MemoryStream())
            {
                memStream.Seek(0, SeekOrigin.Begin);
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    memStream.CopyTo(fs);
                    fs.Flush();
                }
            }
        }

        private string GetSubUri(Uri baseUri, Uri uri) 
            => new string(uri.AbsoluteUri.Except(baseUri.AbsoluteUri).ToArray<char>());

        private string GetStringWithoutInvalidCharacters(string input)
            => Regex.Replace(input ?? string.Empty, "https|http|[<>:;\"|?*]|//", string.Empty);

        private string GetFilteredPath(string path, Uri uri)
            => string.Join(string.Empty, Path.Combine(path, uri.Host) +
                GetStringWithoutInvalidCharacters(uri.LocalPath).Replace("/", @"\"));
    }
}
