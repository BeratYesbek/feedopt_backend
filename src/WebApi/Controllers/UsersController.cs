using System.Globalization;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstracts;
using Core.Entity;
using Core.Extensions;
using Core.Utilities.Result.Concretes;
using Entity.Concretes;
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
        private readonly IUserLocationService _userLocationService;
        private readonly IMapper _mapper;


        public UsersController(IUserService userService, IUserLocationService userLocationService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            _userLocationService = userLocationService;
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] UserUpdateDto userUpdateDto)
        {
            var userResult = _userService.Get(userUpdateDto.Id);
            var user = userResult.Data;
            user.Map(userUpdateDto);
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

    }
}
