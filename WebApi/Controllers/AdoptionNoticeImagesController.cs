using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Concretes;
using Entity.ApiEntity;
using Entity.Concretes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionNoticeImagesController : ControllerBase
    {
        private readonly AdoptionNoticeImageManager adoptionNoticeImageManager = new AdoptionNoticeImageManager();

        [HttpPost("add")]
        public IActionResult Add([FromForm] AdoptionNoticeImageApiEntity adoptionNoticeImageApi)
        {
            var result = adoptionNoticeImageManager.Add(adoptionNoticeImageApi.AdoptionNoticeImage[0],
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
            var result = adoptionNoticeImageManager.Update(adoptionNoticeImageApi.AdoptionNoticeImage,
                adoptionNoticeImageApi.FormFiles);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(AdoptionNoticeImage[] adoptionNoticeImages)
        {
            var result = adoptionNoticeImageManager.Delete(adoptionNoticeImages);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = adoptionNoticeImageManager.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = adoptionNoticeImageManager.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getByAdoptionNoticeId")]
        public IActionResult GetByAdoptionNoticeId(int adoptionNoticeId)
        {
            var result = adoptionNoticeImageManager.GetByAdoptionNoticeId(adoptionNoticeId);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(result);
        }
    }
}
