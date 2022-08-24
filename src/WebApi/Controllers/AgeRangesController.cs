using Business.Abstracts;
using Entity.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgeRangesController : ControllerBase
    {
        private readonly IAgeRangeService _service;

        public AgeRangesController(IAgeRangeService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public IActionResult Add(Age age)
        {
            var result = _service.Add(age);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut("update")]
        public IActionResult Update(Age age)
        {
            var result = _service.Update(age);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Age age)
        {
            var result = _service.Delete(age);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getById/{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.Get(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}