using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Utilities.Result.Abstracts;
using DataAccess.Abstracts;
using Entity.Concretes;
using Entity.Dtos;

namespace DataAccess.Concretes
{
    public class EfChatDal : EfEntityRepositoryBase<Chat, NervioDbContext>, IChatDal
    {
        public List<ChatDto> GetAllByReceiverIdAndSenderId(Expression<Func<Chat, bool>> filter)
        {
            using (NervioDbContext context = new NervioDbContext())
            {
                var result = from chat in context.Chats.Where(filter)
                    join senderUser in context.Users on chat.SenderId equals senderUser.Id
                    join receiverUser in context.Users on chat.ReceiverId equals receiverUser.Id
                    
                    select new ChatDto
                    {
                        Chat = chat,
                        SenderUser = senderUser,
                        ReceiverUser = receiverUser
                    };

                return result.ToList();
            }
        }
    }
}