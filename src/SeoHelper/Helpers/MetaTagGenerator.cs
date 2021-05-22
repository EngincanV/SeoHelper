using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeoHelper.Options;

namespace SeoHelper.Helpers
{
    internal static class MetaTagGenerator
    {
        public static string Generate(MetaTagOptions metaTag)
        {
            var builder = new StringBuilder();
            
            if (!string.IsNullOrWhiteSpace(metaTag.Charset))
            {
                builder.Append(GenerateCharsetMetaTag(metaTag.Charset));
            }
            
            if (!string.IsNullOrWhiteSpace(metaTag.Title))
            {
                builder.AppendLine(GenerateTitle(metaTag.Title));
            }

            builder.AppendLine(GenerateMetaTags(metaTag.MetaTagDescriptions));
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
            if (!metaTagDescriptions.Any())
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            foreach (var (metaTagName, metaTagContent) in metaTagDescriptions)
            {
                builder.AppendLine($"<meta name='{metaTagName}' content='{metaTagContent}' />");
            }

            return builder.ToString();
        }
    }
}