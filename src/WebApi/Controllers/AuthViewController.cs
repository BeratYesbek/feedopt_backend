using Business.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class AuthViewController : Controller
    {
        private readonly IAuthService _authService;
        public AuthViewController(IAuthService authService)
        {
            _authService = authService; 
        }

        [HttpGet("VerifyUser")]
        public IActionResult VerifyUser(string token)
        {
            var stream = token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = tokenS.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || userId == "")
                return View();
            var result = _authService.VerifyEmail(int.Parse(userId));
            return View();
        }
    }
}
