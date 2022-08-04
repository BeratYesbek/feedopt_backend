using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Extensions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Hub.HubFilter
{
    public class HubAuthorizationFilter : IHubFilter
    {
        private readonly IUserService _userService;
        private readonly IUserLocationService _locationService;
        private readonly IHttpContextAccessor _context;
        public HubAuthorizationFilter(IUserService userService, IUserLocationService locationService)
        {
            _locationService = locationService;
            _userService = userService;
            _context = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }
        public async ValueTask<object> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
        {
            SetCurrentUser();
            return await next(invocationContext);
        }


        private void SetCurrentUser()
        {
            var nameIdentifier = _context?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var cultureName = _context?.HttpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
            if (nameIdentifier == null)
                return;
            var userId = int.Parse(nameIdentifier);
            var userResult = _userService.Get(userId);
            var locationResult = _locationService.GetByUserId(userId);

            var latitude = locationResult?.Data?.Latitude != null ? double.Parse(locationResult.Data.Latitude.ToString()) : 0d;
            var longitude = locationResult?.Data?.Longitude != null ? double.Parse(locationResult.Data.Longitude.ToString()) : 0d;
            _context?.HttpContext.SetCurrentUser(userResult.Data, cultureName, latitude, longitude);
        }
    }
}
