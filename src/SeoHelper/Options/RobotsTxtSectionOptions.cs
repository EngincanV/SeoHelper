using System.Collections.Generic;

namespace SeoHelper.Options
{
    public class RobotsTxtSectionOptions
    {
        public string UserAgent { get; set; }

        public List<string> DisallowUrls { get; set; }

        public List<string> AllowUrls { get; set; }
    }
}