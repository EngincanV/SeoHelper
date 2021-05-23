using Microsoft.AspNetCore.Builder;
using SeoHelper.Middlewares;

namespace SeoHelper.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSeoHelper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeoHelperMiddleware>();
        }
    }
}