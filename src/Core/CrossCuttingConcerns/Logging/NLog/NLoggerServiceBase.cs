using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.NLog
{
    public class NLoggerServiceBase
    {
        private readonly Logger _logger = LogManager.GetLogger("databaselogger");

        public bool IsInfoEnabled => _logger.IsInfoEnabled;
        public bool IsDebugEnabled => _logger.IsDebugEnabled;
        public bool IsWarnEnabled => _logger.IsWarnEnabled;
        public bool IsFatalEnabled => _logger.IsFatalEnabled;
        public bool IsErrorEnabled => _logger.IsErrorEnabled;

        public void Info(LogDetail logDetail)
        {
            if (IsInfoEnabled)
                try
                {
                    _logger
                        .WithProperty("UserId", logDetail.UserId)
                        .WithProperty("Email", logDetail.Email)
                        .WithProperty("Claims", logDetail.Claims)
                        .WithProperty("FullName", logDetail.FullName)
                        .WithProperty("MethodName", logDetail.MethodName)
                        .WithProperty("Parameters", JsonSerializer.Serialize(logDetail.Parameters).ToString())
                        .WithProperty("LogDetail", JsonSerializer.Serialize(logDetail).ToString())
                        .Info($"This process has been initialized by {DateTime.Now} and by this user {logDetail.UserId}");
                    Console.WriteLine($"Info Logged");

                }
                catch (Exception exception)
                {
                    Console.WriteLine($"--> ERROR => {exception.Message} \n Trace => {exception.StackTrace}");
                }
     
        }

        public void Debug(object logMessage)
        {
            if (IsDebugEnabled)
                _logger.Warn(JsonSerializer.Serialize(logMessage).ToString());
        }

        public void Warn(object logMessage)
        {
            if (IsWarnEnabled)
                _logger.Warn(JsonSerializer.Serialize(logMessage).ToString());
        }

        public void Fatal(object logMessage)
        {
            if (IsFatalEnabled)
                _logger.Fatal(JsonSerializer.Serialize(logMessage).ToString());
        }

        public void Error(LogDetail logDetail, Exception exception)
        {
            if (IsErrorEnabled)
            {
                _logger
                    .WithProperty("UserId", logDetail.UserId)
                    .WithProperty("Email", logDetail.Email)
                    .WithProperty("Claims", logDetail.Claims)
                    .WithProperty("FullName", logDetail.FullName)
                    .WithProperty("MethodName", logDetail.MethodName)
                    .WithProperty("Parameters", JsonSerializer.Serialize(logDetail.Parameters).ToString())
                    .WithProperty("LogDetail", JsonSerializer.Serialize(logDetail).ToString())
                    .WithProperty("StackTrace", exception.StackTrace)
                    .Error(exception.Message);
            }

        }
    }


}
