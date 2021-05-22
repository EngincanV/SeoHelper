using System;
using System.ComponentModel.DataAnnotations;

namespace SeoHelper.Options
{
    public class SitemapUrlOptions
    {
        public string Url { get; set; }

        public DateTime? LastModificationDate { get; set; }

        [Range(0.0, 1.0)] 
        public double? Priority { get; set; }
    }
}