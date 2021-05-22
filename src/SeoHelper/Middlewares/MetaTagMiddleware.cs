using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
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
            var metaTag = _seoOptions.MetaTags.FirstOrDefault(x => x.Url.ToLowerInvariant() == context.Request.Path.Value);
            if (metaTag != null)
            {
                //TODO: write meta tags into <head> ... </head>
                var generatedMetaTags = MetaTagGenerator.Generate(metaTag);
                await context.Response.WriteAsync(generatedMetaTags);
            }

            await _next(context);
        }
    }
}