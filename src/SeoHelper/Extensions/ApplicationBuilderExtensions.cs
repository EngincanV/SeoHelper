using Microsoft.AspNetCore.Builder;
using SeoHelper.Middlewares;

namespace SeoHelper.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMetaTagMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeoHelperMiddleware>();
        }
    }
}