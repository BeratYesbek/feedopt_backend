using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Middleware
{
    public class VerifyUserRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public VerifyUserRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            ControlRequest(httpContext);
            await _next.Invoke(httpContext);
        }
        
        public void ControlRequest(HttpContext httpContext)
        {
            var controllerName = httpContext.Request.RouteValues["controller"];
            if (controllerName != null)
            {
                if (controllerName.ToString()?.ToLower() == "adverts")
                {
                    AdvertValidateUser(httpContext);
                }
                else if (controllerName.ToString()?.ToLower() == "favoriteadverts")
                {
                    FavoriteAdvertValidateUser(httpContext);
                }
                else if (controllerName.ToString()?.ToLower() == "users")
                {
                    UserValidateUser(httpContext);
                }
                else if (controllerName.ToString()?.ToLower() == "filters")
                {

                }
            }
        }

        public bool UserValidateUser(HttpContext context)
        {
            if (context.Request.Method == "GET")
            {
                var userID = context.Request.Query["userId"].FirstOrDefault();
                return userID != null && CheckCurrentUserId(context, userID);
            }
            else if (context.Request.Method == "PUT")
            {
                var userID = context.Request.Form["id"].FirstOrDefault();
                return userID != null && CheckCurrentUserId(context, userID);

            }
            return false;
        }

        public bool FavoriteAdvertValidateUser(HttpContext context)
        {
            if (context.Request.Method == "GET")
            {
                var userID = context.Request.Query["userId"].FirstOrDefault();
                return userID != null && CheckCurrentUserId(context, userID);
            }
            else if (context.Request.Method == "POST")
            {
                var userID = GetUserIdFromBody(context);
                return userID != null && CheckCurrentUserId(context, userID.ToString());
            }
            return true;
        }

        public void AdvertValidateUser(HttpContext context)
        {
            if (context.Request.Method == "GET")
            {
                var userID = context.Request.Query["userId"];
                
                CheckCurrentUserId(context, userID);
            }
            if (context.Request.Method == "POST" || context.Request.Method == "PUT")
            {
                var userID = context.Request.Form["userId"];
                CheckCurrentUserId(context, userID);
            }
        }

        public string GetUserIdFromBody(HttpContext context)
        {
            context.Request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            context.Request.Body.ReadAsync(buffer, 0, buffer.Count());
            var requestContent = Encoding.UTF8.GetString(buffer);

            context.Request.Body.Position = 0;
            dynamic json = JsonConvert.DeserializeObject(requestContent);
            var userID = json["userId"];
            return userID;
        }

        public bool CheckCurrentUserId(HttpContext context, string userId)
        {
            var currentUserId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != null)
            {
                if (currentUserId != userId)
                    throw new Exception("UserID and Current UserID is not the same");
            }

            return true;
        }  
    }
}
