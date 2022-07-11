using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstracts;
using Core.Entity;
using Core.Entity.Concretes;
using Core.Extensions;
using Core.Utilities.Constants;
using Core.Utilities.IoC;
using Core.Utilities.Result.Concretes;
using Core.Utilities.Security.JWT;
using Entity.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        private readonly IUserService _userService;

        public AuthController(IAuthService authService,IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            var head = Request.Headers;
            var cookie = Request.Cookies;
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {

                result.Data.User = userToLogin.Data;
                HttpContext.SetCookie(new CookieParams
                {
                    AccessToken = result.Data,
                    User = userToLogin.Data,

                });


                Console.WriteLine("--> ");
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
                result.Data.User = registerResult.Data;
                HttpContext.SetCookie(new CookieParams
                {
                    AccessToken = result.Data,
                    User = registerResult.Data,

                });

                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("isLoggedIn")]
        public IActionResult IsLoggedIn()
        {
            return Ok(_authService.IsLoggedIn());
        }

        [HttpPost("forgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] dynamic json)
        {
            var email = json.email ?? (string)json.email;
            if (email == null) return BadRequest("Email is null");
            var result = await _authService.SendResetPasswordCode((string) email);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("verifyCode")]
        public IActionResult VerifyCode([FromBody] dynamic json)
        {
            string code = json.code ?? (string)json.code;
            string email = json.email ?? (string)json.email;
            if (code == null || email == null) return BadRequest("Email and code are null");

            var result = _authService.VerifyCode(code, email);
            if (result.Success)
            {
                var token = _authService.CreateAccessToken(result.Data, DateTime.Now.AddMinutes(5),TokenType.ResetPassword);
                HttpContext.SetCookie(new CookieParams
                {
                    AccessToken = token.Data,
                    User = result.Data
                });
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        
        [HttpPost("resetPassword")]
        public IActionResult ResetPassword([FromBody] dynamic json)
        {
            string password = json.password ?? (string)json.password;
            string passwordConfirmation = json.passwordConfirmation ?? (string)json.passwordConfirmation;
            if (passwordConfirmation == null || password == null) return BadRequest("Passwords are nullable");
     
            var result = _authService.ResetPassword(password, passwordConfirmation);
            if (result.Success)
            {
                HttpContext.DeleteCookies();
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] dynamic json)
        {

            string oldPassword = json.oldPassword ?? (string)json.oldPassword;
            string password = json.password ?? (string)json.password;
            string passwordConfirmation = json.passwordConfirmation ?? (string)json.passwordConfirmation;
            if (passwordConfirmation == null || password == null || oldPassword == null) return BadRequest("Passwords are nullable");

            var result = await _authService.ChangePassword(oldPassword, password, passwordConfirmation);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.DeleteCookies();
            return Ok(new SuccessResult());
        }

    }
}