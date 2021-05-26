using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SeoHelper.Helpers
{
    internal static class HtmlHelper
    {
        private const string Pattern = @"<head>((?:.|\n|\r)+?)<\/head>";

        internal static async Task<Stream> AppendMetaTagsToHeadSectionAsync(HttpContext context, RequestDelegate next, string generatedMetaTags)
        {
            var stream = context.Response.Body;
            
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;
                await next(context);
                memoryStream.Seek(0, SeekOrigin.Begin);

                await ReplaceHtmlHeadSectionAsync(memoryStream, stream, generatedMetaTags);
            }

            return stream;
        }

        private static async Task ReplaceHtmlHeadSectionAsync(MemoryStream ms, Stream stream, string metaTags)
        {
            using (var reader = new StreamReader(ms))
            {
                var html = await reader.ReadToEndAsync();
                var match = Regex.Match(html, pattern: Pattern);
                    
                if (match.Success)
                {
                    var headTagBetweenText = match.Groups[1].Value;
                    html = html.Replace(headTagBetweenText, headTagBetweenText + "\n" + metaTags);
                }

                await CopyHtmlContentAsync(stream, html);
            }
        }

        private static async Task CopyHtmlContentAsync(Stream stream, string html)
        {
            var bytes = Encoding.UTF8.GetBytes(html);
            
            using (var memoryStream = new MemoryStream(bytes))
            {
                memoryStream.Write(bytes, 0, bytes.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.CopyToAsync(stream);
            }
        }
    }
}