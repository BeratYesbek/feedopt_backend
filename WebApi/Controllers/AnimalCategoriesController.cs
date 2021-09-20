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
    }
}
