using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.BusinessAspect.SecurityAspect;
using Business.Security.Role;
using Core.Extensions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Hub.HubFilter
{
    [Authorize]
    public class HubAuthorizationFilter : IHubFilter
    {
        private readonly Authorize _authorize;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HubAuthorizationFilter(Authorize authorize)
        {
            _authorize = authorize;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        public async ValueTask<object> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
        {
           
            var httpContext = invocationContext.Context.GetHttpContext();
            SetCurrentUser(httpContext);
            return await next(invocationContext);
        }
        public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
        {
            var httpContext = context.Context.GetHttpContext();
            SetCurrentUser(httpContext);            return next(context);
        }
        public Task OnDisconnectedAsync(HubLifetimeContext context, Exception exception, Func<HubLifetimeContext, Exception, Task> next)
        {
            var httpContext = context.Context.GetHttpContext();
            SetCurrentUser(httpContext);
            return next(context, exception);
        }


        private void SetCurrentUser(HttpContext context)
        {
            _authorize.SetCurrentUser(context);
        }
    }
}
