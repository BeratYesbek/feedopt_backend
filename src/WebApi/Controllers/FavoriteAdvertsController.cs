using Business.Abstracts;
using Entity.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteAdvertsController : Controller
    {
        private readonly IFavoriteAdvertService _favoriteAdvertService;

        public FavoriteAdvertsController(IFavoriteAdvertService favoriteAdvertService)
        {
            _favoriteAdvertService = favoriteAdvertService;
        }
        
        [HttpPost("add")]
        public IActionResult Add(FavoriteAdvert favorite)
        {
            favorite.CreatedAt = System.DateTime.Now;
            var result = _favoriteAdvertService.Add(favorite);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPut("update")]
        public IActionResult Update(FavoriteAdvert favorite)
        {
            var result = _favoriteAdvertService.Update(favorite);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPost("delete")]
        public IActionResult Delete(FavoriteAdvert favorite)
        {
            var result = _favoriteAdvertService.Delete(favorite);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _favoriteAdvertService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _favoriteAdvertService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpGet("getAllDetailByUserId/{userId}")]
        public IActionResult GetAllDetailByUserId(int userId)
        {
            var result = _favoriteAdvertService.GetAllDetailByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}