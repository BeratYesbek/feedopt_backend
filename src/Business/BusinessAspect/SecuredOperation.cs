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
using Core.CustomExceptions;


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

            var cookieEmail = _httpContextAccessor.HttpContext.Request.Cookies["Email"];

            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            var exp = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(t => t.Type == "exp");
            var email = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;

            //var name = _httpContextAccessor.HttpContext.User.Identity.Name;
            //var userid = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;


            if (exp == null)
            {
                throw new AuthenticationFailedException("Your token expiration is up");
            }

            if (email != cookieEmail)
            {
                throw new AuthenticationException("Your cookie email is not correct please sign in again");
            }

            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }

            throw new AuthenticationFailedException("You have no authorization");
        }

    }
}