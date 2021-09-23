using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Concretes;
using Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissingDeclarationsController : ControllerBase
    {
        public MissingDeclarationsController()
        {
        }

        [HttpPost("Add")]
        public IActionResult Add(MissingDeclaration missingDeclaration)
        {
            var result = new MissingDeclarationManager().Add(missingDeclaration);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetAll")]
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