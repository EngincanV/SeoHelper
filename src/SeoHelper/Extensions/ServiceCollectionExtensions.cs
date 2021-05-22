using System;
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
    }
}