using System;
using System.Net;
using Core.Entity.Concretes;
using Core.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.Extensions
{
    public static class CookieExtension
    {

        public static void SetCookie(this HttpContext httpContext, CookieParams cookieParams)
        {
            httpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cookieParams.User.PreferredLanguage.ToString())),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            httpContext.Response.Cookies.Append(CookieKey.AuthorizationKey, cookieParams.AccessToken.Token,
                new CookieOptions { Expires = cookieParams.AccessToken.Expiration, HttpOnly = true, Secure = true });

            httpContext.Response.Cookies.Append(CookieKey.ExpireKey, cookieParams.AccessToken.Token,
                new CookieOptions { Expires = cookieParams.AccessToken.Expiration, HttpOnly = true, Secure = true });

            httpContext.Response.Cookies.Append(CookieKey.Email, cookieParams.User.Email,
                new CookieOptions { Expires = cookieParams.AccessToken.Expiration,HttpOnly = true,Secure = true});
        }

    }
}