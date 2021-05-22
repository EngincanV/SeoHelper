using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SeoHelper.Options
{
    public class SitemapUrlOptions
    {
        [NotNull] 
        public string Url { get; private set; }

        public DateTime? LastModificationDate { get; private set; }

        [Range(0.0, 1.0)] 
        public double? Priority { get; private set; }

        public SitemapUrlOptions(
            string url,
            DateTime? lastModificationDate = null,
            double? priority = null
        )
        {
            Url = url;
            LastModificationDate = lastModificationDate;
            Priority = priority;
        }
    }
}