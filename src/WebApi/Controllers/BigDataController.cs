using System.Threading;
using Business.Abstracts;
using Entity.concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BigDataController : ControllerBase
    {
        private IAdoptionNoticeService _adoptionNoticeService;
        public BigDataController(IAdoptionNoticeService adoptionNoticeService)
        {
            _adoptionNoticeService = adoptionNoticeService;
        }

        [HttpPost("/Add")]
        public IActionResult Add([FromForm] AdoptionNotice adoptionNotice)
        {
            for (int i = 0; i < 1000; i++)
            {
                _adoptionNoticeService.Add(adoptionNotice);
                adoptionNotice.Id = 0;
                //Thread.Sleep(1);
            }

            return Ok();
        }
    }
}
