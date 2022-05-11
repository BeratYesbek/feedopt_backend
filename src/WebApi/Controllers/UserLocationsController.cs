using Business.Abstracts;
using Core.Entity;
using Entity.Concretes;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UserLocationsController : ControllerBase
    {
        private readonly IUserLocationService _userLocationService;
        public UserLocationsController(IUserLocationService userLocationService)
        {
            _userLocationService = userLocationService;
        }

        [HttpPut("/update")]
        public IActionResult Update(UserLocation userLocation)
        {
            var result = _userLocationService.Add(userLocation);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("/getById")]
        public IActionResult GetByUserId(int userId)
        {
            var result = _userLocationService.GetById(userId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
