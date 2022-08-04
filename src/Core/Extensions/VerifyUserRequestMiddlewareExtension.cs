using Core.Utilities.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Core.Extensions
{
    public static class VerifyUserRequestMiddlewareExtension
    {
        public static IApplicationBuilder VerifyUserRequest(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<VerifyUserRequestMiddleware>();
        }
    }
}
