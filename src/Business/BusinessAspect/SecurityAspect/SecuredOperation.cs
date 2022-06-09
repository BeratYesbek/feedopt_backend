using System;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Authentication;
using Business.Concretes;
using Core.CustomExceptions;
using Core.Entity.Concretes;
using DataAccess.Concretes;
using Microsoft.AspNetCore.Localization;
using Core.Entity;

namespace Business.BusinessAspect
{
    public class SecuredOperation : MethodInterception
    {
        private readonly string[] _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            // a sample jwt encoded token string which is supposed to be extracted from 'Authorization' HTTP header in your Web Api controller
            var nameIdentifier = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var cookieEmail = _httpContextAccessor.HttpContext?.Request.Cookies["Email"];

            var roleClaims = _httpContextAccessor.HttpContext?.User.ClaimRoles();
            var exp = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(t => t.Type == "exp");
            var cultureName = _httpContextAccessor.HttpContext?.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

            if (nameIdentifier is not null)
                SetCurrentUser(nameIdentifier, cultureName?.Split("|")[0].Split("=")[1]);

            if (exp == null)
                throw new AuthenticationFailedException("Your session has been expired.");


            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                    return;
            }
           throw new AuthenticationFailedException("You have no authorization.");
        }
        private static User SetCurrentUser(string nameIdentifier, string cultureName)
        {
            var result = new UserManager(new EfUserDal()).Get(int.Parse(nameIdentifier));
            if (result.Success)
            {
                CurrentUser.User = result.Data;
                var locationResult = new UserLocationManager(new EfUserLocationDal()).GetById(CurrentUser.User.Id);
                if (locationResult.Success)
                {
                    CurrentUser.Longitude = Decimal.ToDouble(locationResult.Data.Longitude);
                    CurrentUser.Latitude = Decimal.ToDouble(locationResult.Data.Latitude);
                    CurrentUser.CultureName = cultureName;
                }
            }
            return result.Data;
        }

    }


}