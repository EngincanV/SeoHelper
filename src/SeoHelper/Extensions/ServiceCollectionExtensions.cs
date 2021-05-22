using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeoHelper.Options;

namespace SeoHelper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSeo(this IServiceCollection services, Action<SeoOptions> options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            services.AddOptions<SeoOptions>().Configure(options);
            return services;
        }

        public static IServiceCollection AddSeo(this IServiceCollection services, IConfiguration configuration, string? sectionName = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            sectionName = string.IsNullOrWhiteSpace(sectionName) ? "SeoOptions" : sectionName;
            services.Configure<SeoOptions>(configuration.GetSection(sectionName).Bind);
            return services;
        }
    }
}