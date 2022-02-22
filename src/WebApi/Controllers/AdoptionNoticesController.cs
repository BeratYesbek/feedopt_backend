using Microsoft.AspNetCore.Mvc;
using Business.Abstracts;
using Entity.concretes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionNoticesController : ControllerBase
    {
        private readonly IAdoptionNoticeService _adoptionNoticeService;

        public AdoptionNoticesController(IAdoptionNoticeService adoptionNoticeService)
        {
            _adoptionNoticeService = adoptionNoticeService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] AdoptionNotice adoptionNotice)
        {
            var result = _adoptionNoticeService.Add(adoptionNotice);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] AdoptionNotice adoptionNotice)
        {
            var result = _adoptionNoticeService.Update(adoptionNotice);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("delete")]
        public IActionResult Delete(AdoptionNotice adoptionNotice)
        {
            var result = _adoptionNoticeService.Delete(adoptionNotice);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = _adoptionNoticeService.Get(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _adoptionNoticeService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

       [HttpGet("getAllAdoptionNoticeDetail")]
        public IActionResult GetAllAdoptionNoticeDetail(int pageNumber)
        {
            var result = _adoptionNoticeService.GetAllAdoptionNoticeDetail(pageNumber);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("GetAdoptionNoticeDetailById")]
        public IActionResult GetAdoptionNoticeDetailById(int id)
        {
            var result = _adoptionNoticeService.GetAdoptionNoticeDetailById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}