using System;
using System.Net;
using Core.Entity.Concretes;
using Core.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Net.Http.Headers;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Core.Extensions
{
    public static class CookieExtension
    {

        public static void SetCookie(this HttpContext httpContext, CookieParams cookieParams)
        {
            

            httpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cookieParams.User.PreferredLanguage.ToString())),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None }
            );
            httpContext.Response.Cookies.Append(CookieKey.AuthorizationKey, cookieParams.AccessToken.Token,
                new CookieOptions { Expires = cookieParams.AccessToken.Expiration, HttpOnly = true, Secure = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None });

            httpContext.Response.Cookies.Append(CookieKey.ExpireKey, cookieParams.AccessToken.Token,
                new CookieOptions { Expires = cookieParams.AccessToken.Expiration, HttpOnly = true, Secure = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None });

            httpContext.Response.Cookies.Append(CookieKey.Email, cookieParams.User.Email,
                new CookieOptions { Expires = cookieParams.AccessToken.Expiration,HttpOnly = true,Secure = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None });          
        }

        public static void DeleteCookies(this HttpContext httpContext)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTimeOffset.Now;
            option.Secure = true;
            option.IsEssential = true;
            option.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
            httpContext.Response.Cookies.Append(CookieKey.AuthorizationKey, string.Empty, option);
            httpContext.Response.Cookies.Append(CookieKey.ExpireKey, string.Empty, option);
            httpContext.Response.Cookies.Append(CookieKey.Email, string.Empty, option);
            httpContext.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, string.Empty, option);


            httpContext.Response.Cookies.Delete(CookieKey.AuthorizationKey);
            httpContext.Response.Cookies.Delete(CookieKey.ExpireKey);
            httpContext.Response.Cookies.Delete(CookieKey.Email);
            httpContext.Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);

            
        }

      


    }
}