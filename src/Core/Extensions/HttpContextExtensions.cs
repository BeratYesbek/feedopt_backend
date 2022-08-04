using Core.Entity.Concretes;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// payload contains some properties of user
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="user"></param>
        /// <param name="payload"></param>
        /// <param name="cultureName"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public static void SetCurrentUser(this HttpContext httpContext,User user,string cultureName,double latitude,double longitude)
        {
            CurrentUser.User = user;
            CurrentUser.CultureName = cultureName?.Split("|")[0].Split("=")[1];
            CurrentUser.Latitude = latitude;
            CurrentUser.Longitude = longitude;
        }
    }
}
