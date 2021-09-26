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
    public class MissingDeclarationImagesController : ControllerBase
    {
        private MissingDeclarationImageManager missingDeclarationImageManager = new MissingDeclarationImageManager();

        [HttpPost("add")]
        public IActionResult Add([FromForm] MissingDeclarationImageApiEntity missingDeclarationImageApiEntity)
        {
            var result = missingDeclarationImageManager.Add(missingDeclarationImageApiEntity.MissingDeclarationImage[0],
                missingDeclarationImageApiEntity.FormFiles);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update([FromForm] MissingDeclarationImageApiEntity missingDeclarationImageApiEntity)
        {
            var result = missingDeclarationImageManager.Update(missingDeclarationImageApiEntity.MissingDeclarationImage,
                missingDeclarationImageApiEntity.FormFiles);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(MissingDeclarationImage[] missingDeclarationImage)
        {
            var result = missingDeclarationImageManager.Delete(missingDeclarationImage);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = missingDeclarationImageManager.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = missingDeclarationImageManager.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetByMissingDeclarationId")]
        public IActionResult GetByMissingDeclarationId(int missingDeclarationId)
        {
            var result = missingDeclarationImageManager.GetByMissingDeclarationId(missingDeclarationId);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(result);
        }
    }
}