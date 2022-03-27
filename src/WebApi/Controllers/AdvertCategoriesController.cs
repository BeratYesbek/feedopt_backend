using Business.Abstracts;
using Entity.concretes;
using Entity.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertCategoriesController : ControllerBase
    {
        private readonly IAdvertCategoryService _advertCategory;

        public AdvertCategoriesController(IAdvertCategoryService advertCategory)
        {
            _advertCategory = advertCategory;
        }

        [HttpPost("add")]
        public IActionResult Add(AdvertCategory category)
        {
            var result = _advertCategory.Add(category);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpPut("update")]
        public IActionResult Update(AdvertCategory category)
        {
            var result = _advertCategory.Update(category);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpDelete("delete")]
        public IActionResult Delete(AdvertCategory category)
        {
            var result = _advertCategory.Delete(category);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpGet("getById/{id}")]
        public IActionResult Get(int id)
        {
            var result = _advertCategory.Get(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _advertCategory.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
