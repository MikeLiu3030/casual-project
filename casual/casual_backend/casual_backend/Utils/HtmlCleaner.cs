using HtmlAgilityPack;
using System.Net;
namespace casual_backend.Utils
{
    public static class HtmlCleaner
    {
        public static string ExtractPlainText(string? htmlContent)
        {
            if (string.IsNullOrWhiteSpace(htmlContent))
                return string.Empty;

            // 1. Load HTML content
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);

            // 2. Extract plain text
            string plainText = htmlDoc.DocumentNode.InnerText;

            // 3. Decode HTML entity
            string decodedText = WebUtility.HtmlDecode(plainText);

            // 4. Clean up extra whitespace 
            decodedText = System.Text.RegularExpressions.Regex.Replace(decodedText, @"\s+", " ").Trim();

            return decodedText;


        }
    }
}
