using Business.Abstracts.Translations;
using Entity.Concretes.Translations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Translations
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertCategoryTranslationsController : ControllerBase
    {
        private readonly IAdvertCategoryTranslationService _advertCategoryTranslationService;
        public AdvertCategoryTranslationsController(IAdvertCategoryTranslationService advertCategoryTranslationService)
        {
            _advertCategoryTranslationService = advertCategoryTranslationService;
        }

        [HttpPost("add")]
        public IActionResult Add(AdvertCategoryTranslation translation)
        {
            var result = _advertCategoryTranslationService.Add(translation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();   
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _advertCategoryTranslationService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
