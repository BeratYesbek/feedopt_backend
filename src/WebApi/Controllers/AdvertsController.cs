
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstracts;
using Core.Entity.Abstracts;
using Core.Extensions;
using Core.Utilities.IoC;
using Entity.Concretes;
using Entity.Dtos;
using Entity.Dtos.Filter;
using Hangfire;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Controllers
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {
        private readonly IAdvertService _advertService;

        private readonly IMapper _mapper;

        public AdvertsController(IAdvertService advertService, IMapper mapper)
        {
            _advertService = advertService;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] AdvertCreateDto advertCreateDto)
        {
            var advert = _mapper.Map<Advert>(advertCreateDto);
            var advertImage = _mapper.Map<AdvertImage>(advertCreateDto);
            var location = _mapper.Map<Location>(advertCreateDto);
            advert.Status = Status.Pending;
            var result = await _advertService.Add(advert, advertImage, advertCreateDto.Files, location);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("updateStatus")]
        public IActionResult UpdateStatus(Advert advert)
        {
            var result = _advertService.UpdateStatus(advert);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromForm] AdvertUpdateDto advertUpdateDto,int id)
        {
            var advertImage = _mapper.Map<AdvertImage>(advertUpdateDto);
            var location = _mapper.Map<Location>(advertUpdateDto);

             var dataResult = _advertService.Get(id);
             var advert = dataResult.Data;
             advert.Map(advertUpdateDto);
       
            var result = await _advertService.Update(advert, advertImage, advertUpdateDto.Files, location);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Advert advert)
        {
            var result =  _advertService.Delete(advert);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getAllAdvertDetail/{pageNumber}")]
        public IActionResult GetAllAdvertDetail(int pageNumber)
        {
            var result = _advertService.GetAllAdvertDetail(pageNumber);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getAllAdvertDetailByFilter/{pageNumber}")]
        public IActionResult GetAllAdvertDetailByFilter([FromQuery] AdvertFilterDto filter,int pageNumber)
        {
            var result = _advertService.GetAllAdvertDetailsByFilter(filter,pageNumber);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _advertService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _advertService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getDetailById/{id}")]
        public IActionResult GetDetailById(int id)
        {
            var result = _advertService.GetAdvertDetailById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("/getByUserId")]
        public IActionResult GetByUserId(int userId, int pageNumber)
        {
            var result = _advertService.GetAdvertDetailByUserId(userId, pageNumber);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getAllAdvertByDistance/{pageNumber}")]
        public IActionResult GetAllAdvertByDistance(int pageNumber)
        {
            var result = _advertService.GetAllAdvertByDistance(pageNumber);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
