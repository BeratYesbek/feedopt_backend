using Business.Abstracts;
using Core.Entity.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationsController : ControllerBase
    {
        private readonly ITranslationService _translationService;

        public TranslationsController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [HttpPost("add")]
        public IActionResult Add(Translation translation)
        {
            var result = _translationService.Add(translation);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Translation translation)
        {
            var result = _translationService.Update(translation);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Translation translation)
        {
            var result = _translationService.Delete(translation);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _translationService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getByType/{type}")]
        public IActionResult GetByType(string type)
        {
            var result = _translationService.GetByType(type);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _translationService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
