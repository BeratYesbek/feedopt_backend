using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging;
using Microsoft.Extensions.Logging;
using NLog;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var logger = LogManager.GetLogger("databaselogger");
            logger.Info(GetLogDetail());
            return View();
        }

        private LogDetail GetLogDetail()
        {
            var logParameters = new List<LogParameter>();
            logParameters.Add(new LogParameter()
            {
                Name = "dsa0,",
                Value = "dsadsadsadas"
            });


            var logDetail = new LogDetail
            {
                MethodName = "invocation.Method.Name",
                Parameters = logParameters,
                FullName = "invocation.Method.DeclaringType?.FullName",
            };

            return logDetail;
        }
    }
}