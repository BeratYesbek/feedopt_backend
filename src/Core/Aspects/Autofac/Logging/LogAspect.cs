﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.CrossCuttingConcerns.Logging.NLog;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly NLoggerServiceBase _nLoggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType == typeof(LoggerServiceBase))
                _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);

            if (loggerService.BaseType == typeof(NLoggerServiceBase))
                _nLoggerServiceBase = (NLoggerServiceBase)Activator.CreateInstance(loggerService);

            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();


        }

        protected override void OnBefore(IInvocation invocation)
        {
            if (_loggerServiceBase != null)
                _loggerServiceBase.Info(GetLogDetail(invocation));
            if (_nLoggerServiceBase != null)
                _nLoggerServiceBase.Info((LogDetail)GetLogDetail(invocation));
        }

        protected override void OnException(IInvocation invocation, Exception exception)
        {
              if (_loggerServiceBase != null)
              {
                  _loggerServiceBase.Error(new LogWithException
                  {
                      Exception = exception,
                      ExceptionMessage = exception.Message
                  });
              }
              if (_nLoggerServiceBase != null)
                  _nLoggerServiceBase.Error((LogDetail)GetLogDetail(invocation), exception);
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name,
                });
            }

            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                Parameters = logParameters,
                FullName = invocation.Method.DeclaringType?.FullName,
                Email = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                UserId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                Claims = string.Join(",", _httpContextAccessor.HttpContext?.User.ClaimRoles() ?? throw new InvalidOperationException())

            };
            return logDetail;
        }


    }
}
