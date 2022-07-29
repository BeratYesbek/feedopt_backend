using Business.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;
        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetDashboard()
        {
            var result = _service.GetDashboard();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);  
        }
    }
}
