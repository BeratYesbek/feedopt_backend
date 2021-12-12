using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Concretes;
using Entity.ApiEntity;
using Entity.Concretes;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionNoticeImagesController : ControllerBase
    {
        private readonly IAdoptionNoticeImageService _adoptionNoticeImageService;

        public AdoptionNoticeImagesController(IAdoptionNoticeImageService adoptionNoticeImageService)
        {
            _adoptionNoticeImageService = adoptionNoticeImageService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] AdoptionNoticeImageApiEntity adoptionNoticeImageApi)
        {
            if (adoptionNoticeImageApi.AdoptionNoticeImage != null)
            {
                AdoptionNoticeImage[] adoptionNoticeImages =
                    JsonConvert.DeserializeObject<AdoptionNoticeImage[]>(adoptionNoticeImageApi.AdoptionNoticeImage);
            }
       
            var result = _adoptionNoticeImageService.Add(null,
                adoptionNoticeImageApi.FormFiles);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] AdoptionNoticeImageApiEntity adoptionNoticeImageApi)
        {
            AdoptionNoticeImage[] adoptionNoticeImages =
                JsonConvert.DeserializeObject<AdoptionNoticeImage[]>(adoptionNoticeImageApi.AdoptionNoticeImage);
            var result = _adoptionNoticeImageService.Update(adoptionNoticeImages,
                adoptionNoticeImageApi.FormFiles);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("delete")]
        public IActionResult Delete(AdoptionNoticeImage[] adoptionNoticeImages)
        {
            var result = _adoptionNoticeImageService.Delete(adoptionNoticeImages);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = _adoptionNoticeImageService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _adoptionNoticeImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getByAdoptionNoticeId")]
        public IActionResult GetByAdoptionNoticeId(int adoptionNoticeId)
        {
            var result = _adoptionNoticeImageService.GetByAdoptionNoticeId(adoptionNoticeId);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(result);
        }
    }
}