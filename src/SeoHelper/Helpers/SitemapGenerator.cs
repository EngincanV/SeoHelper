using System.Linq;
using System.Text;
using SeoHelper.Options;

namespace SeoHelper.Helpers
{
    internal struct SitemapGenerator
    {
        internal static string Generate(SitemapOptions sitemap)
        {
            if (sitemap.Urls == null || !sitemap.Urls.Any())
            {
                return string.Empty;
            }
            
            var builder = new StringBuilder();
            builder.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
            
            foreach (var sitemapUrl in sitemap.Urls)
            {
                builder.AppendLine("<url>");
                builder.AppendLine($"<loc>{sitemapUrl.Url}</loc>");
                
                if (sitemapUrl.LastModificationDate.HasValue)
                {
                    builder.AppendLine($"<lastmod>{sitemapUrl.LastModificationDate}</lastmod>");
                }

                if (sitemapUrl.Priority.HasValue)
                {
                    builder.AppendLine($"<priority>{sitemapUrl.Priority}</priority>");
                }
                
                builder.AppendLine("</url>");
            }

            builder.AppendLine("</urlset>");
            return builder.ToString();
        }
    }
}