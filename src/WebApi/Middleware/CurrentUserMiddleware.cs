using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Entity.Concretes;
using Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace WebApi.Middleware
{
    public class CurrentUserMiddleware
    {
        private readonly IUserService _userService;
        private readonly IUserLocationService _locationService;
        private readonly RequestDelegate _request;

        public CurrentUserMiddleware(IUserLocationService locationService, IUserService userService, RequestDelegate request)
        {
            _userService = userService;
            _locationService = locationService;
            _request = request;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            SetCurrentUser(httpContext);
            await _request.Invoke(httpContext);
        }

        private void SetCurrentUser(HttpContext httpContext)
        {
            var nameIdentifier = httpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var cultureName = httpContext?.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
            if (nameIdentifier == null)
                return;
            var userId = int.Parse(nameIdentifier);
            var userResult = _userService.Get(userId);
            var locationResult = _locationService.GetByUserId(userId);

            var latitude = locationResult?.Data?.Latitude != null ? double.Parse(locationResult.Data.Latitude.ToString()) : 0d;
            var longitude = locationResult?.Data?.Longitude != null ? double.Parse(locationResult.Data.Longitude.ToString()) : 0d;
            httpContext.SetCurrentUser(userResult.Data,cultureName,latitude,longitude);
        }
    }
}
