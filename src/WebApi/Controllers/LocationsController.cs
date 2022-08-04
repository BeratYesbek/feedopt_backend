using Microsoft.AspNetCore.Mvc;
using Business.Abstracts;
using Entity.Concretes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost("add")]
        public IActionResult Add(Location location)
        {
            var result = _locationService.Add(location);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut("update")]
        public IActionResult Update(Location location)
        {
            var result = _locationService.Update(location);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Location location)
        {
            var result = _locationService.Delete(location);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

    }
}
