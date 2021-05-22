using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SeoHelper.Options
{
    public class MetaTagOptions
    {
        [NotNull]
        public string RelativeUrl { get; set; }
        public string Title { get; set; }
        public string Charset { get; set; }
        
        [NotNull]
        public Dictionary<string, string> MetaTagDescriptions { get; set; }
    }
}