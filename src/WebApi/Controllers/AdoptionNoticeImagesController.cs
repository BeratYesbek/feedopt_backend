﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Concretes;
using Entity.Concretes;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionNoticeImagesController : ControllerBase
    {
        private readonly IAdoptionNoticeImageService _adoptionNoticeImageService;

        public AdoptionNoticeImagesController(IAdoptionNoticeImageService adoptionNoticeImageService)
        {
            _adoptionNoticeImageService = adoptionNoticeImageService;
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = _adoptionNoticeImageService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _adoptionNoticeImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getByAdoptionNoticeId")]
        public IActionResult GetByAdoptionNoticeId(int adoptionNoticeId)
        {
            var result = _adoptionNoticeImageService.GetByAdoptionNoticeId(adoptionNoticeId);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(result);
        }
    }
}