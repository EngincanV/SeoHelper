using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SeoHelper.Helpers
{
    internal static class HtmlHelper
    {
        internal static async Task<Stream> AppendMetaTagsToHeadSectionAsync(HttpContext context, RequestDelegate next, string generatedMetaTags)
        {
            var stream = context.Response.Body;
            using (var ms = new MemoryStream())
            {
                context.Response.Body = ms;
                await next(context);
                ms.Seek(0, SeekOrigin.Begin);
                
                using (var reader = new StreamReader(ms))
                {
                    var html = await reader.ReadToEndAsync();
                    var match = Regex.Match(html, @"<head>((?:.|\n|\r)+?)<\/head>");
                    var headTagBetweenText = match.Groups[1].Value;
                    html = html.Replace(headTagBetweenText, headTagBetweenText + "\n" + generatedMetaTags);

                    var bytes = Encoding.UTF8.GetBytes(html);
                    using (var memoryStream = new MemoryStream(bytes))
                    {
                        memoryStream.Write(bytes, 0, bytes.Length);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        await memoryStream.CopyToAsync(stream);
                    }
                }
            }

            return stream;
        }
    }
}