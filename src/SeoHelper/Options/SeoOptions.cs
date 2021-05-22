using System.Collections.Generic;

namespace SeoHelper.Options
{
    public class SeoOptions
    {
        public List<MetaTagOptions> MetaTags { get; set; }
        
        public SitemapOptions Sitemap { get; set; }

        public RobotsTxtOptions RobotsTxt { get; set; }
    }
}