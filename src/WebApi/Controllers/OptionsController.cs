using Business.Abstracts;
using Business.Concretes;
using DataAccess;
using DataAccess.Concretes;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Net.Mail;

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


        [HttpGet]
        public  IActionResult StartMailer()
        {
            var colors = new AppDbContext().Colors.Include(t => t.ColorTranslations).ToList();
            return Ok(colors);
        }
    }
}
