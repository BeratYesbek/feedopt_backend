using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Concretes;
using Entity.concretes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionNoticeController : ControllerBase
    {
        private readonly IAdoptionNoticeService _adoptionNoticeService;

        public AdoptionNoticeController(IAdoptionNoticeService adoptionNoticeService)
        {
            _adoptionNoticeService = adoptionNoticeService;
        }

        [HttpPost("add")]
        public IActionResult Add(AdoptionNotice adoptionNotice)
        {
            var result = _adoptionNoticeService.Add(adoptionNotice);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("update")]
        public IActionResult Update(AdoptionNotice adoptionNotice)
        {
            var result = _adoptionNoticeService.Update(adoptionNotice);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("delete")]
        public IActionResult Delete(AdoptionNotice adoptionNotice)
        {
            var result = _adoptionNoticeService.Delete(adoptionNotice);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = _adoptionNoticeService.Get(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _adoptionNoticeService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}