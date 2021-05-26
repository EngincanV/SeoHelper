using System.Linq;
using System.Text;
using SeoHelper.Options;

namespace SeoHelper.Helpers
{
    internal static class RobotsTxtGenerator
    {
        internal static string Generate(RobotsTxtOptions robotsTxt, string sitemapUrl = null)
        {
            if (robotsTxt.RobotsTxtSections == null || !robotsTxt.RobotsTxtSections.Any())
            {
                return string.Empty;
            }
            
            var builder = new StringBuilder();

            foreach (var robotsTxtSection in robotsTxt.RobotsTxtSections)
            {
                builder.AppendLine($"User-agent: {robotsTxtSection.UserAgent}");
                
                robotsTxtSection.DisallowUrls.ForEach(disallowUrl => builder.AppendLine($"Disallow: {disallowUrl}"));
                robotsTxtSection.AllowUrls.ForEach(allowUrl => builder.AppendLine($"Allow: {allowUrl}"));

                builder.AppendLine();
            }

            if (robotsTxt.DisplaySitemapUrl && !string.IsNullOrWhiteSpace(sitemapUrl))
            {
                builder.AppendLine($"Sitemap: {sitemapUrl}");
            }
            
            return builder.ToString();
        }
    }
}