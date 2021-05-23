﻿using System.Linq;
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
                context.Response.Body = await HtmlHelper.AppendMetaTagsToHeadSectionAsync(context, _next, generatedMetaTags);
                return;
            }
            
            await _next(context);
        }
    }
}