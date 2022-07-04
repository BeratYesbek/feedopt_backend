using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Core.CustomExceptions;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _requestDelegate;
        private readonly IWebHostEnvironment _env;
        private  IConfiguration Configuration { get; }

        public ExceptionMiddleware(RequestDelegate requestDelegate, IConfiguration configuration,IWebHostEnvironment env)
        {
            _requestDelegate = requestDelegate;
            Configuration = configuration;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception exception)
            {
                if (exception.GetType() == typeof(ValidationException))
                {
                    ThrowValidationException(httpContext, exception);
                }
                else
                {
                    await HandleExceptionAsync(httpContext, exception);

                }
            }
        }

        private void ThrowValidationException(HttpContext httpContext, Exception exception)
        {
            var message = exception.Message;
            Uri redirectURI = null;
            if (_env.IsDevelopment())
            {
                redirectURI = new Uri($"{Configuration.GetSection("ErrorsUrl")["ValidationError"]}?&message={message}");
            }
            else
            {
                redirectURI = new Uri($"{Configuration.GetSection("ErrorsUrl")["ValidationError"]}?&message={message}");
            }
            httpContext.Response.Redirect(redirectURI.AbsoluteUri);

        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            string message = exception.Message;
            if (exception.GetType() == typeof(ValidationException))
            {
                message = exception.Message;
            }
            else if (exception.GetType() == typeof(AuthenticationFailedException) || exception.GetType() == typeof(AuthenticationException))
            {
                message = exception.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message,
                Description = exception.InnerException?.Message + " --> Details => " + exception.StackTrace
            }.ToString());
        }
    }
}
