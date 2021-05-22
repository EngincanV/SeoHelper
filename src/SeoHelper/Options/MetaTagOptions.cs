using System.Collections.Generic;

namespace SeoHelper.Options
{
    public class MetaTagOptions
    {
        public string RelativeUrl { get; set; }
        public string Title { get; set; }
        public string Charset { get; set; }
        public Dictionary<string, string> MetaTagDescriptions { get; set; }
    }
}