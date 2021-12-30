using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Business.Abstracts;
using Core.Extensions;
using Core.Utilities.Constants;
using Entity.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userForLoginDto);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                CookieExtension.SecureHttpOnly(CookieKey.AuthorizationKey, result.Data.Token);
                CookieExtension.SecureHttpOnly(CookieKey.ExpireKey, result.Data.Expiration.ToString());
                CookieExtension.SecureHttpOnly(CookieKey.Email, userForLoginDto.Email);
                return Ok(result);
            }

            return BadRequest();
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
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

    }
}