using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SeoHelper.Options
{
    public class MetaTagOptions
    {
        [NotNull]
        public string Url { get; private set; }
        public string Title { get; set; }
        public string Charset { get; set; }
        public Dictionary<string, string> MetaTagDescriptions { get; set; }

        public MetaTagOptions(string url)
        {
            Url = url;
        }
    }
}