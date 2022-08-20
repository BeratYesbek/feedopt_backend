using Microsoft.AspNetCore.Mvc;
using Business.Abstracts;
using Entity.concretes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalCategoriesController : ControllerBase
    {
        private readonly IAnimalCategoryService _animalCategoryService;

        public AnimalCategoriesController(IAnimalCategoryService animalCategoryService)
        {
            _animalCategoryService = animalCategoryService;
        }

        [HttpPost("add")]
        public IActionResult Add(AnimalCategory animalCategory)
        {
            var result = _animalCategoryService.Add(animalCategory);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpPut("update")]
        public IActionResult Update(AnimalCategory animalCategory)
        {
            var result = _animalCategoryService.Update(animalCategory);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpDelete("delete")]
        public IActionResult Delete(AnimalCategory animalCategory)
        {
            var result = _animalCategoryService.Delete(animalCategory);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpGet("getById/{id}")]
        public IActionResult Get(int id)
        {
            var result = _animalCategoryService.Get(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _animalCategoryService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}