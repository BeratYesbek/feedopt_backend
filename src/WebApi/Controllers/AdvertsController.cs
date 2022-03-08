
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstracts;
using Entity.Concretes;
using Entity.Dtos;
using Entity.Dtos.Filter;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
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

            var result = await _advertService.Add(advert, advertImage, advertCreateDto.Files, location);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] AdvertUpdateDto advertUpdateDto)
        {
            var advert = _mapper.Map<Advert>(advertUpdateDto);
            var advertImage = _mapper.Map<AdvertImage>(advertUpdateDto);
            var location = _mapper.Map<Location>(advertUpdateDto);

            var result = await _advertService.Update(advert, advertImage, advertUpdateDto.Files, location);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Advert advert)
        {
            var result = await _advertService.Delete(advert);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getAllAdvertDetail")]
        public IActionResult GetAllAdvertDetail(int pageNumber, double latitude, double longitude)
        {
            var result = _advertService.GetAllAdvertDetail(pageNumber, latitude, longitude);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getAllAdvertDetailByFilter")]
        public IActionResult GetAllAdvertDetailByFilter([FromQuery] AdvertFilterDto filter, int pageNumber)
        {
            var result = _advertService.GetAllAdvertDetailsByFilter(filter, pageNumber);
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
    }
}
