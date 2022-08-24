using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using Business.Abstracts;
using Business.Concretes;
using Core.Extensions;
using DataAccess.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Business.BusinessAspect.SecurityAspect;

public class Authorize
{
    private readonly IUserService _userService;
    private readonly IUserLocationService _locationService;
    public Authorize()
    {
        _userService = new UserManager(new EfUserDal());
        _locationService = new UserLocationManager(new EfUserLocationDal());
    }

    public void SetCurrentUser(HttpContext httpContext)
    {
        var nameIdentifier = httpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var cultureName = httpContext?.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
        if (nameIdentifier == null)
            throw new AuthenticationException("You have no auth");
        var userId = int.Parse(nameIdentifier);
        var userResult = _userService.Get(userId);
        var locationResult = _locationService.GetByUserId(userId);
        var latitude = locationResult?.Data?.Latitude != null ? double.Parse(locationResult.Data.Latitude.ToString()) : 0d;
        var longitude = locationResult?.Data?.Longitude != null ? double.Parse(locationResult.Data.Longitude.ToString()) : 0d;
        httpContext?.SetCurrentUser(userResult.Data,cultureName,latitude,longitude);
    }
}