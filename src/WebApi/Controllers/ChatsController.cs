using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Business.Abstracts;
using Entity.Concretes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatsController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("getAllByReceiverIdAndSenderId")]
        public async Task<IActionResult> GetAllByReceiverIdAndSenderId(int senderId, int receiverId)
        {
            var result = await _chatService.GetAllByReceiverIdAndSenderId(senderId, receiverId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getAllLastMessages")]
        public async Task<IActionResult> GetAllLastMessages(int id)
        {
            var result = await _chatService.GetAllLastMessages(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}