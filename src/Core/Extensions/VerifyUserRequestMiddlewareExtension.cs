using Core.Utilities.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
