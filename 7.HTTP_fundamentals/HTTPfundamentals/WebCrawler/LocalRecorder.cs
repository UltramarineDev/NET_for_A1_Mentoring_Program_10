using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;


namespace WebCrawler
{
    public class LocalRecorder
    {
        public void Record(string path, Uri uri, HtmlDocument document)
        {
            var pathToDirectory = Path.Combine(path, uri.Host) + uri.LocalPath.Replace("/", @"\");
            var name = document.DocumentNode.SelectSingleNode("title") + ".html";
            var filePath = Path.Combine(pathToDirectory, name);

            using (var memStream = new MemoryStream())
            {
                document.Save(memStream);
                Directory.CreateDirectory(pathToDirectory);
                var fileStream = File.Create(filePath);
                memStream.CopyTo(fileStream);
            }
        }
    }
}
