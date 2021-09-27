using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Concretes;
using Entity.Concretes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly LocationManager locationManager = new LocationManager();
        [HttpPost("add")]
        public IActionResult Add(Location location)
        {
            var result =  locationManager.Add(location);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("update")]
        public IActionResult Update(Location location)
        {
            var result =  locationManager.Update(location);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("delete")]
        public IActionResult Delete(Location location)
        {
            var result = locationManager.Delete(location);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = locationManager.Get(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = locationManager.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
