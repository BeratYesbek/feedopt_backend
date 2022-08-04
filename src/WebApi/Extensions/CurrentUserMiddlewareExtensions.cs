using Microsoft.AspNetCore.Builder;
using WebApi.Middleware;

namespace WebApi.Extensions
{
    public static class CurrentUserMiddlewareExtensions
    {

        public static IApplicationBuilder UserCurrentUserMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CurrentUserMiddleware>();
        }
    }
}
