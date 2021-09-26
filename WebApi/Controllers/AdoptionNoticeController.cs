﻿using Microsoft.AspNetCore.Http;
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

        [HttpPost("add")]
        public IActionResult Add(AdoptionNotice adoptionNotice)
        {
            var result = new AdoptionNoticeManager().Add(adoptionNotice);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("update")]
        public IActionResult Update(AdoptionNotice adoptionNotice)
        {
            var result = new AdoptionNoticeManager().Update(adoptionNotice);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("delete")]
        public IActionResult Delete(AdoptionNotice adoptionNotice)
        {
            var result = new AdoptionNoticeManager().Delete(adoptionNotice);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = new AdoptionNoticeManager().Get(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getAll")]
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