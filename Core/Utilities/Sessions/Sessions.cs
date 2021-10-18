using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.Sessions
{
    public static class Sessions
    {
        static HttpContextAccessor _httpContextAccessor =
            (HttpContextAccessor) ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        public static void AddSession(string key, string value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, value);
        }

        public static void AddSession(string key, int value)
        {
            _httpContextAccessor.HttpContext.Session.SetInt32(key, value);
        }

        public static int? GetSessionIntValue(string key)
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32(key);
        }

        public static string GetSessionStringValue(string key)
        {
            return _httpContextAccessor.HttpContext.Session.GetString(key);
        }
    }
}