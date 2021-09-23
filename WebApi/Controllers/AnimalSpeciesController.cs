using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Entity.concretes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalSpeciesController : ControllerBase
    {
        [HttpPost("Add")]
        public IActionResult Add(AnimalSpecies animalSpecies)
        {
            var result = new AnimalSpeciesManager().Add(animalSpecies);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = new AnimalSpeciesManager().Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = new AnimalSpeciesManager().GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}