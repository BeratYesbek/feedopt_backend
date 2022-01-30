﻿using System;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Business.Abstracts;
using Core.Entity;
using Core.Entity.Concretes;
using Core.Extensions;
using Core.Utilities.Constants;
using Entity.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IUserService userService,IMapper mapper)
        {
            _authService = authService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                var user = _userService.GetByMail(userToLogin.Data.Email);
                result.Data.User = user.Data;
                var cookieParams = new CookieParams();
                cookieParams.AccessToken = result.Data;
                cookieParams.User = user.Data;
                HttpContext.SetCookie(cookieParams);
                return Ok(result);
            }
            return BadRequest(userToLogin);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var existsUser = _authService.UserExists(userForRegisterDto.Email);

            if (!existsUser.Success)
            {
                return BadRequest(existsUser.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                var user = _userService.GetByMail(registerResult.Data.Email);
                result.Data.User = user.Data;
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Update")]
        public IActionResult Update(User user)
        {

            return Ok(_userService.Update(user));
        }

        [HttpGet("verify")]
        public IActionResult Verify(string param)
        {
            return Ok("Doğrulandı");
        }

    }
}