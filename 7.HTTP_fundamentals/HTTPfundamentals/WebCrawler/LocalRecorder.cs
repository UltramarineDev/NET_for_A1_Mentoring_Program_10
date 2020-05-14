using System;
using System.Drawing;
using System.Drawing.Imaging;
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

            var image = DownloadImageFromUrl(uri.AbsoluteUri);
            var imgFormat = GetImageFormat(uri);
           
            if (image != null)
            {
                image.Save(filePath, imgFormat);
            }
        }

        private Image DownloadImageFromUrl(string imageUrl)
        {
            Image image;
            try
            {
                var webRequest = System.Net.WebRequest.Create(imageUrl);
                webRequest.Timeout = 30000;

                var webResponse = webRequest.GetResponse();
                var stream = webResponse.GetResponseStream();
                image = Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception)
            {
                return null;
            }

            return image;
        }

        private string GetSubUri(Uri baseUri, Uri uri)
            => new string(uri.AbsoluteUri.Except(baseUri.AbsoluteUri).ToArray<char>());

        private string GetStringWithoutInvalidCharacters(string input)
            => Regex.Replace(input ?? string.Empty, "https|http|[<>:;\"|?*]|//", string.Empty);

        private string GetFilteredPath(string path, Uri uri)
            => string.Join(string.Empty, Path.Combine(path, uri.Host) +
                GetStringWithoutInvalidCharacters(uri.LocalPath).Replace("/", @"\"));

        private ImageFormat GetImageFormat(Uri uri)
        {
            var imgFormat = ImageFormat.Png;

            switch (uri.Segments.Last().Split('.')[1])
            {
                case "jpeg":
                    imgFormat = ImageFormat.Jpeg;
                    break;
                case "gif":
                    imgFormat = ImageFormat.Gif;
                    break;
            }

            return imgFormat;
        }
    }
}
