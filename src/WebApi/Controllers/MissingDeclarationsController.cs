﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Concretes;
using Entity;
using Entity.concretes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissingDeclarationsController : ControllerBase
    {
        private readonly IMissingDeclarationService _missingDeclarationService;

        public MissingDeclarationsController(IMissingDeclarationService missingDeclarationService)
        {
            _missingDeclarationService = missingDeclarationService;
        }

        [HttpPost("add")]
        public IActionResult Add(MissingDeclaration missingDeclaration)
        {
            var result = _missingDeclarationService.Add(missingDeclaration);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(MissingDeclaration missingDeclaration)
        {
            var result = _missingDeclarationService.Update(missingDeclaration);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpPost("delete")]
        public IActionResult Delete(MissingDeclaration missingDeclaration)
        {
            var result = _missingDeclarationService.Delete(missingDeclaration);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = _missingDeclarationService.Get(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _missingDeclarationService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}