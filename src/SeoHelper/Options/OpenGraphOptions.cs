using System.Collections.Generic;

namespace SeoHelper.Options
{
    public class OpenGraphOptions
    {
        public TwitterOptions Twitter { get; set; }

        public List<OpenGraphPageOptions> Pages { get; set; }
    }
}