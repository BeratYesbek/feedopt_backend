using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstracts;
using Core.Entity;
using Core.Utilities.Result.Concretes;
using Entity.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] UserUpdateDto userUpdateDto)
        {
            var user = _mapper.Map<User>(userUpdateDto);
            var result = await _userService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _userService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("updateLocation")]
        public IActionResult UpdateLocation(int latitude,int longitude,int userId)
        {
            return Ok();
        }

    }
}
