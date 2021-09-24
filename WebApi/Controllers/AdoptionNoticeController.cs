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
    public class AdoptionNoticeController : ControllerBase
    {

        public AdoptionNoticeController()
        {
            
        }

        [HttpPost("Add")]
        public IActionResult Add(AdoptionNotice adoptionNotice)
        {
            var result = new AdoptionNoticeManager().Add(adoptionNotice);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("Update")]
        public IActionResult Update(AdoptionNotice adoptionNotice)
        {
            var result = new AdoptionNoticeManager().Update(adoptionNotice);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("Delete")]
        public IActionResult Delete(AdoptionNotice adoptionNotice)
        {
            var result = new AdoptionNoticeManager().Delete(adoptionNotice);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("GetById")]
        public IActionResult Get(int id)
        {
            var result = new AdoptionNoticeManager().Get(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll(AdoptionNotice adoptionNotice)
        {
            var result = new AdoptionNoticeManager().GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}