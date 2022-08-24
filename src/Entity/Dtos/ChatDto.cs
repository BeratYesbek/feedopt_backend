
using Core.Entity.Concretes;
using Entity.Concretes;

namespace Entity.Dtos
{
    public class ChatDto
    {
        public Chat Chat { get; set; }
        public UserDto SenderUser { get; set; }
        public UserDto ReceiverUser { get; set; }
    }
}