using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Entity.Concretes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportsController : ControllerBase
    {
        private readonly ISupportService _supportService;
        public SupportsController(ISupportService supportService)
        {
            _supportService = supportService;
        }

        [HttpPost("Add")]
        public IActionResult Add([FromForm] Support ticket)
        {
            var result = _supportService.Add(ticket);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);

        }
    }
}
