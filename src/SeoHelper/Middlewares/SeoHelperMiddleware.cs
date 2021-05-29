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
                await GenerateSitemapXmlPageAsync(context);
                return;
            }

            if (context.Request.Path.Value == "/robots.txt")
            {
                await GenerateRobotsTxtPageAsync(context);
                return;
            }

            var metaTag = _seoOptions?.MetaTags?.FirstOrDefault(x => x.RelativeUrl.ToLowerInvariant().EnsureStartsWith('/') == context.Request.Path.Value);
            if (metaTag != null)
            {
                var generatedMetaTags = MetaTagGenerator.Generate(metaTag);
                context.Response.Body = await HtmlHelper.AppendMetaTagsToHeadSectionAsync(context, _next, generatedMetaTags);
                return;
            }

            if (_seoOptions?.OpenGraph?.Twitter != null)
            {
                //TODO: check response type. it should be html
                var generateTwitterCardMetaTags = MetaTagGenerator.GenerateTwitterCardMetaTags(_seoOptions.OpenGraph.Twitter);
                context.Response.Body = await HtmlHelper.AppendMetaTagsToHeadSectionAsync(context, _next, generateTwitterCardMetaTags);
                return;
            }
            
            var openGraphMetaTag = _seoOptions?.OpenGraph?.Pages?.FirstOrDefault(x => x.Url.ToLowerInvariant().EnsureStartsWith('/') == context.Request.Path.Value);
            if (openGraphMetaTag != null)
            {
                var generatedOpenGraphMetaTags = MetaTagGenerator.GenerateOpenGraphTags(openGraphMetaTag);
                context.Response.Body = await HtmlHelper.AppendMetaTagsToHeadSectionAsync(context, _next, generatedOpenGraphMetaTags);
                return;
            }

            await _next(context);
        }

        private async Task GenerateSitemapXmlPageAsync(HttpContext context)
        {
            if (_seoOptions?.Sitemap != null)
            {
                context.Response.ContentType = "application/xml";
                await context.Response.WriteAsync(SitemapGenerator.Generate(_seoOptions.Sitemap));
            }
        }

        private async Task GenerateRobotsTxtPageAsync(HttpContext context)
        {
            if (_seoOptions?.RobotsTxt != null)
            {
                var sitemapUrl = $"{context.Request.Scheme}://{context.Request.Host.ToString().EnsureEndsWith('/')}sitemap.xml";
                await context.Response.WriteAsync(RobotsTxtGenerator.Generate(_seoOptions.RobotsTxt, sitemapUrl));
            }
        }
    }
}