using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Concretes;
using Entity.concretes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalCategoriesController : ControllerBase
    {

        [HttpPost("Add")]
        public IActionResult Add(AnimalCategory animalCategory)
        {
            var result = new AnimalCategoryManager().Add(animalCategory);
            if (result.Success)
            {
                return Ok();

            }

            return BadRequest();
        }


        [HttpPost("Update")]
        public IActionResult Update(AnimalCategory animalCategory)
        {
            var result = new AnimalCategoryManager().Update(animalCategory);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpPost("Delete")]
        public IActionResult Delete(AnimalCategory animalCategory)
        {
            var result = new AnimalCategoryManager().Delete(animalCategory);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpGet("GetById")]
        public IActionResult Get(int id)
        {
            var result = new AnimalCategoryManager().Get(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = new AnimalCategoryManager().GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
