﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Localization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        private readonly IStringLocalizer<ErrorsController> _localizer;
        public ErrorsController(IStringLocalizer<ErrorsController> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet("validationException")]
       
        public IActionResult ValidationException(string message)
        {
            var splittedMessages = message.Split("*");
            var errorList = new List<string>();
            foreach (var splittedMessage in splittedMessages)
            {
                errorList.Add(_localizer[splittedMessage]);
            }
            errorList.RemoveAt(0);
            return BadRequest(new ErrorDetails
            {
                Message = "Validation Errors",
                Messages = errorList,
                Status = false
            });
        }
    }
}