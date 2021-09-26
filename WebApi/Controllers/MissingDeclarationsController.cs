using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Concretes;
using Entity;
using Entity.concretes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissingDeclarationsController : ControllerBase
    {
        public MissingDeclarationsController()
        {

        }

        [HttpPost("add")]
        public IActionResult Add(MissingDeclaration missingDeclaration)
        {
            var result = new MissingDeclarationManager().Add(missingDeclaration);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(MissingDeclaration missingDeclaration)
        {
            var result = new MissingDeclarationManager().Update(missingDeclaration);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpPost("delete")]
        public IActionResult Delete(MissingDeclaration missingDeclaration)
        {
            var result = new MissingDeclarationManager().Delete(missingDeclaration);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = new MissingDeclarationManager().Get(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


     

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = new MissingDeclarationManager().GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}