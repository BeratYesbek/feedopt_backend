using Business.Abstracts.Translations;
using Entity.Concretes.Translations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Translations
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalCategoryTranslationsController : ControllerBase
    {
        private readonly IAnimalCategoryTranslationService _animalCategoryTranslationService;

        public AnimalCategoryTranslationsController(IAnimalCategoryTranslationService animalCategoryTranslationService)
        {
            _animalCategoryTranslationService = animalCategoryTranslationService;
        }

        [HttpPost("add")]
        public IActionResult Add(AnimalCategoryTranslation translation)
        {
            var result = _animalCategoryTranslationService.Add(translation);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _animalCategoryTranslationService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}