using Business.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly IOptionService _optionService;
        public OptionsController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpGet("getOptions")]
        public IActionResult GetOptions()
        {
            var result = _optionService.GetOptions();
            if (result.Success) 
                return Ok(result);

            return BadRequest(result);
        }
    }
}
