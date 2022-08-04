using Business.Abstracts;
using Entity.Concretes.Translations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Translations
{
    [Route("api/trasnlations/[controller]")]
    [ApiController]
    public class ColorTranslationsController : ControllerBase
    {

        private readonly IColorTranslationService _colorTranslationService;
        public ColorTranslationsController(IColorTranslationService colorTranslationService)
        {
            _colorTranslationService = colorTranslationService;
        }

        [HttpPost("add")]
        public IActionResult Add(ColorTranslation colorTranslation)
        {
            var result = _colorTranslationService.Add(colorTranslation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _colorTranslationService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
