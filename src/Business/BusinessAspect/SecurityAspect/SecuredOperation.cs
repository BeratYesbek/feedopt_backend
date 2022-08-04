using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Core.CustomExceptions;
using Core.Utilities.Security.JWT;
using Microsoft.IdentityModel.Tokens;

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
            var roleClaims = _httpContextAccessor.HttpContext?.User.ClaimRoles();
            var exp = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(t => t.Type == "exp");
            var tokenType = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(t => t.Type == "TokenType")?.Value;

            if (tokenType != null && !tokenType.Equals(TokenType.Standard.ToString()))
                throw new SecurityTokenException("Invalid Token");
            
            if (exp == null)
                throw new AuthenticationFailedException("Your session has been expired.");

            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                    return;
            }

            throw new AuthenticationFailedException("You have no authorization.");
        }

    }


}