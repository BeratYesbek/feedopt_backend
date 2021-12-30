using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Concretes;
using Entity;
using Entity.ApiEntity;
using Entity.Concretes;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissingDeclarationImagesController : ControllerBase
    {

        private readonly IMissingDeclarationImageService _missingDeclarationImageService;

        public MissingDeclarationImagesController(IMissingDeclarationImageService missingDeclarationImageService)
        {
            _missingDeclarationImageService = missingDeclarationImageService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] MissingDeclarationImageApiEntity missingDeclarationImageApiEntity)
        {
            MissingDeclarationImage[] missingDeclarations =
                JsonConvert.DeserializeObject<MissingDeclarationImage[]>(missingDeclarationImageApiEntity
                    .MissingDeclarationImage);


            var result = _missingDeclarationImageService.Add(missingDeclarations[0],
                missingDeclarationImageApiEntity.FormFiles);

            if (result.Success)
            {
                return Ok(result);
            }
           
            return BadRequest();
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] MissingDeclarationImageApiEntity missingDeclarationImageApiEntity)
        {
            MissingDeclarationImage[] missingDeclarations =
                JsonConvert.DeserializeObject<MissingDeclarationImage[]>(missingDeclarationImageApiEntity
                    .MissingDeclarationImage);
            var result = _missingDeclarationImageService.Update(missingDeclarations,
                 missingDeclarationImageApiEntity.FormFiles);
 
             if (result.Success)
             {
                 return Ok(result);
             }
          
            return BadRequest();
        }

        [HttpPost("delete")]
        public IActionResult Delete(MissingDeclarationImage[] missingDeclarationImage)
        {
            var result = _missingDeclarationImageService.Delete(missingDeclarationImage);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = _missingDeclarationImageService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _missingDeclarationImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetByMissingDeclarationId")]
        public IActionResult GetByMissingDeclarationId(int missingDeclarationId)
        {
            var result = _missingDeclarationImageService.GetByMissingDeclarationId(missingDeclarationId);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(result);
        }
    }
}