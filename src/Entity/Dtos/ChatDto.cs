
using Core.Entity.Concretes;
using Entity.Concretes;

namespace Entity.Dtos
{
    public class ChatDto
    {
        public Chat Chat { get; set; }

        public User SenderUser { get; set; }

        public User ReceiverUser { get; set; }
    }
}