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
    public class SeoHelperMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SeoOptions _seoOptions;

        public SeoHelperMiddleware(RequestDelegate next, IOptions<SeoOptions> seoOptions)
        {
            _next = next;
            _seoOptions = seoOptions.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value == "/sitemap.xml")
            {
                context.Response.ContentType = "application/xml";
                await context.Response.WriteAsync(SitemapGenerator.Generate(_seoOptions.Sitemap));
                return;
            }

            if (context.Request.Path.Value == "/robots.txt")
            {
                var sitemapUrl = context.Request.Scheme + "://" + context.Request.Host.ToString().EnsureEndsWith('/') + "sitemap.xml";
                await context.Response.WriteAsync(RobotsTxtGenerator.Generate(_seoOptions.RobotsTxt, sitemapUrl));
                return;
            }

            var metaTag = _seoOptions.MetaTags.FirstOrDefault(x => x.RelativeUrl.ToLowerInvariant().EnsureStartsWith('/') == context.Request.Path.Value);
            if (metaTag != null)
            {
                var generatedMetaTags = MetaTagGenerator.Generate(metaTag);
                context.Response.Body = await AppendMetaTagsToHeadSection(context, generatedMetaTags);
                return;
            }
            
            await _next(context);
        }

        private async Task<Stream> AppendMetaTagsToHeadSection(HttpContext context, string generatedMetaTags)
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