using System;
using Core.Entity.Concretes;
using Core.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Core.Extensions
{
    public static class CookieExtension
    {

        public static void SetCookie(this HttpContext httpContext, CookieParams cookieParams)
        {

            var options = new CookieOptions { Expires = cookieParams.AccessToken.Expiration, HttpOnly = true, Secure = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None, IsEssential= true };
            httpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cookieParams.User.PreferredLanguage.ToString())),
                options
            );
            httpContext.Response.Cookies.Append(CookieKey.AuthorizationKey, cookieParams.AccessToken.Token,
                options);

            httpContext.Response.Cookies.Append(CookieKey.ExpireKey, cookieParams.AccessToken.Token,
                options
               );
            httpContext.Response.Cookies.Append(CookieKey.Email, cookieParams.User.Email,
                options);          
        }

        public static void SetNewToken(this HttpContext httpContext, string newToken)
        {
            var options = new CookieOptions { Expires = DateTime.Now.AddYears(1), HttpOnly = true, Secure = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None, IsEssential = true };
            httpContext.Response.Cookies.Append(CookieKey.NotifyToken,newToken);

        }

        public static void DeleteCookies(this HttpContext httpContext)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTimeOffset.Now;
            option.Secure = true;
            option.IsEssential = true;
           
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