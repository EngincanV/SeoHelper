using System.Collections.Generic;
using SeoHelper.Enums;

namespace SeoHelper.Options
{
    public class MetaTagOptions
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Charset { get; set; }
        public Dictionary<MetaTagName, string> MetaTagDescriptions { get; set; }
    }
}