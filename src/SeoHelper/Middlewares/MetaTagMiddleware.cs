using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SeoHelper.Extensions;
using SeoHelper.Helpers;
using SeoHelper.Options;

namespace SeoHelper.Middlewares
{
    public class MetaTagMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SeoOptions _seoOptions;

        public MetaTagMiddleware(RequestDelegate next, IOptions<SeoOptions> seoOptions)
        {
            _next = next;
            _seoOptions = seoOptions.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var metaTag = _seoOptions.MetaTags.FirstOrDefault(x => x.RelativeUrl.ToLowerInvariant().EnsureStartsWith('/') == context.Request.Path.Value);
            
            if (metaTag != null)
            {
                var generatedMetaTags = MetaTagGenerator.Generate(metaTag);
                context.Response.Body = await ReplaceHtmlHeadTagWithMetaTags(context, generatedMetaTags);
                return;
            }
            
            await _next(context);
        }

        private async Task<Stream> ReplaceHtmlHeadTagWithMetaTags(HttpContext context, string generatedMetaTags)
        {
            var stream = context.Response.Body;
            using (var buffer = new MemoryStream())
            {
                context.Response.Body = buffer;
                await _next(context);
                buffer.Seek(0, SeekOrigin.Begin);
                
                using (var reader = new StreamReader(buffer))
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