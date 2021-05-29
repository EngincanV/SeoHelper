using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using SeoHelper.Constants;
using SeoHelper.Options;

namespace SeoHelper.Helpers
{
    internal static class MetaTagGenerator
    {
        public static string Generate(MetaTagOptions metaTag)
        {
            var builder = new StringBuilder();
            
            if (!string.IsNullOrWhiteSpace(metaTag.Title))
            {
                builder.AppendLine(GenerateTitle(metaTag.Title));
            }
            
            if (!string.IsNullOrWhiteSpace(metaTag.Charset))
            {
                builder.AppendLine(GenerateCharsetMetaTag(metaTag.Charset));
            }
            
            builder.AppendLine(GenerateMetaTags(metaTag.MetaTagDescriptions));
            return builder.ToString();
        }

        public static string GenerateOpenGraphTags(OpenGraphPageOptions openGraphPage)
        {
            var builder = new StringBuilder();

            if (openGraphPage != null)
            {
                builder.AppendLine(GenerateOpenGraphMetaTags(openGraphPage));
            }

            return builder.ToString();
        }

        public static string GenerateTwitterCardMetaTags(TwitterOptions twitter)
        {
            var twitterCardDescriptions = new Dictionary<string, string>
            {
                { TwitterCardName.Site, twitter.Site }, 
                { TwitterCardName.Creator, twitter.Creator }
            };

            return GenerateMetaTags(twitterCardDescriptions);
        }

        private static string GenerateOpenGraphMetaTags([NotNull] OpenGraphPageOptions openGraphPage)
        {
            var builder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(openGraphPage.Url))
            {
                var metaTagDescriptions = new Dictionary<string, string>
                {
                    { OpenGraphTagName.Title, openGraphPage.OgTitle },
                    { OpenGraphTagName.Type, openGraphPage.OgType },
                    { OpenGraphTagName.Image, openGraphPage.OgImage },
                    { OpenGraphTagName.Url, openGraphPage.OgUrl }
                };

                builder.AppendLine(GenerateMetaTags(metaTagDescriptions));
            }

            return builder.ToString();
        }

        private static string GenerateCharsetMetaTag(string charset)
        {
            return $"<meta charset={charset} />";
        }

        private static string GenerateTitle(string title)
        {
            return $"<title>{title}</title>";
        }

        private static string GenerateMetaTags(Dictionary<string, string> metaTagDescriptions)
        {
            if (metaTagDescriptions == null || !metaTagDescriptions.Any())
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            foreach (var (metaTagName, metaTagContent) in metaTagDescriptions)
            {
                if (!string.IsNullOrWhiteSpace(metaTagName) && !string.IsNullOrWhiteSpace(metaTagContent))
                {
                    builder.AppendLine($"<meta name='{metaTagName}' content='{metaTagContent}' />");
                }
            }

            return builder.ToString();
        }
    }
}