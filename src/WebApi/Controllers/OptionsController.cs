using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Business.Abstracts;
using Business.Concretes;
using Core.Utilities.Cloud.Aws;
using DataAccess;
using DataAccess.Concretes;
using Entity.Dtos;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly IOptionService _optionService;


        public OptionsController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpGet("getOptions")]
        public IActionResult GetOptions()
        {
            var result = _optionService.GetOptions();
            if (result.Success) 
                return Ok(result);

            return BadRequest(result);
        }
    }
}
