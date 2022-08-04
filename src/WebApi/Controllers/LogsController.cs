using Business.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;

        }

        [HttpGet("/getAll")]
        public IActionResult GetAll()
        {
            var result = _logService.GetAll();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("/getLogDetails")]
        public IActionResult GetLogDetails()
        {
            var result = _logService.GetLogDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
