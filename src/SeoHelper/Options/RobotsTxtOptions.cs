using System.Collections.Generic;

namespace SeoHelper.Options
{
    public class RobotsTxtOptions
    {
        public bool DisplaySitemapUrl { get; set; }

        public List<RobotsTxtSectionOptions> RobotsTxtSections { get; set; }
    }
}