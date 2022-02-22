using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {
        

        public AdvertsController()
        {
            
        }

        public IActionResult Add()
        {
            return Ok();
        }

        public IActionResult Update()
        {
            return Ok();
        }

        public IActionResult Delete()
        {
            return Ok();
        }

        public IActionResult GetAllDetail()
        {
            return Ok();
        }

        public IActionResult GetAll()
        {
            return Ok();
        }

        public IActionResult GetById()
        {
            return Ok();
        }

        public IActionResult GetDetailById()
        {
            return Ok();
        }
    }
}
