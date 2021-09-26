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

        [HttpPost("add")]
        public IActionResult Add(AnimalCategory animalCategory)
        {
            var result = new AnimalCategoryManager().Add(animalCategory);
            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest();
        }


        [HttpPost("update")]
        public IActionResult Update(AnimalCategory animalCategory)
        {
            var result = new AnimalCategoryManager().Update(animalCategory);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpPost("delete")]
        public IActionResult Delete(AnimalCategory animalCategory)
        {
            var result = new AnimalCategoryManager().Delete(animalCategory);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = new AnimalCategoryManager().Get(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpGet("getAll")]
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
