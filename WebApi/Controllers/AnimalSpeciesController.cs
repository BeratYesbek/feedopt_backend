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
        [HttpPost("add")]
        public IActionResult Add(AnimalSpecies animalSpecies)
        {
            var result = new AnimalSpeciesManager().Add(animalSpecies);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("update")]
        public IActionResult Update(AnimalSpecies animalSpecies)
        {
            var result = new AnimalSpeciesManager().Update(animalSpecies);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("delete")]
        public IActionResult Delete(AnimalSpecies animalSpecies)
        {
            var result = new AnimalSpeciesManager().Delete(animalSpecies);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = new AnimalSpeciesManager().Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getAll")]
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