using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;
using Core.Extensions;
using Core.Utilities.Sessions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;


namespace Business.BusinessAspect
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            var exp = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(t => t.Type == "exp");

            if (exp == null)
            {
                throw new Exception("Your token expiration is up");
            }

            foreach (var role in _roles)
            {
   
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }

            throw new Exception("You have no authorization");
        }
    }
}