using Business.Abstracts;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltersController : ControllerBase
    {
        private readonly IFilterService _filterService;
        public FiltersController(IFilterService filterService)
        {
            _filterService = filterService;

        }

        [HttpPost("add")]
        public IActionResult Add(Filter filter)
        {
            var result = _filterService.Add(filter);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut("update")]
        public IActionResult Update(Filter filter)
        {
            var result = _filterService.Update(filter);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Filter filter)
        {
            var result = _filterService.Delete(filter);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getById/{id}")]
        public IActionResult Get(int id)
        {
            var result = _filterService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getByFilterType/{filterType}")]
        public IActionResult GetByFilterType(string filterType)
        {
            var result = _filterService.GetByFilterType(filterType);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _filterService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


    }
}
