using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Core.Entity.Concretes;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Result.Abstracts;
using Core.Utilities.Result.Concretes;
using Core.Utilities.Security.Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private IConfiguration Configuration { get; }
        private readonly TokenOptions _tokenOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims,DateTime dateTime = default,TokenType tokenType = TokenType.Standard)
        {
            _accessTokenExpiration = dateTime == default ? DateTime.Now.AddDays(_tokenOptions.AccessTokenExpiration) : dateTime;
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims,tokenType);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims,TokenType tokenType)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims,tokenType),
                signingCredentials: signingCredentials
            );


            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims,TokenType tokenType)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddTokenType(tokenType);
            claims.AddName($"{user.FullName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }

        public IDataResult<dynamic> GetIdentifier(string tokenType)
        {
            var type = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(t => t.Type == nameof(TokenType))?.Value;

            if (type == null) return new ErrorDataResult<dynamic>(null, "Invalid Token and Identifier");

            if (!tokenType.Equals(type)) return new ErrorDataResult<dynamic>(null, "Invalid Token and Identifier");

            var nameIdentifier = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (nameIdentifier != null)
                return new SuccessDataResult<dynamic>(nameIdentifier, "Token is valid");

            return new ErrorDataResult<dynamic>(null, "Invalid Token and Identifier");
        }
    }
}