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
        private readonly IAnimalSpeciesService _animalSpeciesService;

        public AnimalSpeciesController(IAnimalSpeciesService animalSpeciesService)
        {
            _animalSpeciesService = animalSpeciesService;
        }

        [HttpPost("add")]
        public IActionResult Add(AnimalSpecies animalSpecies)
        {
            var result = _animalSpeciesService.Add(animalSpecies);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("update")]
        public IActionResult Update(AnimalSpecies animalSpecies)
        {
            var result = _animalSpeciesService.Update(animalSpecies);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("delete")]
        public IActionResult Delete(AnimalSpecies animalSpecies)
        {
            var result = _animalSpeciesService.Delete(animalSpecies);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _animalSpeciesService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _animalSpeciesService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}