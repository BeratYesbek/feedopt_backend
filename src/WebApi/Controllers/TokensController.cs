using System.Threading.Tasks;
using Business.Abstracts;
using Core.Entity.Concretes;
using Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokenService _service;
        public TokensController(ITokenService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Token token)
        {
            var result = await _service.Add(token);
            
            if (result.Success)
            {
                HttpContext.SetNewToken(result.Data.UserToken);
                return Ok(result);
            }

            var tokenResult  = await _service.GetByCurrentUser();
            if (tokenResult.Success)
            {
                HttpContext.SetNewToken(tokenResult.Data.UserToken);
                return Ok(tokenResult);

            }
            return BadRequest(result);
        }
    }
}
