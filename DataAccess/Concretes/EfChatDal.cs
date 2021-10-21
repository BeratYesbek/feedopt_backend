using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<ChatDto> GetAllLastMessages(Expression<Func<Chat, bool>> filter, int id)
        {
            using (NervioDbContext context = new NervioDbContext())
            {
                var result = from chat in context.Chats.Where(filter).ToList()
                        .GroupBy(c => c.SenderId == id ? c.ReceiverId : c.SenderId)
                        .Select(c => c.OrderByDescending(t => t.Date).First()).ToList()
                    join senderUser in context.Users on chat.ReceiverId equals senderUser.Id
                    join receiverUser in context.Users on chat.SenderId equals receiverUser.Id
                    select new ChatDto
                    {
                        SenderUser = senderUser,
                        ReceiverUser = receiverUser,
                        Chat = chat
                    };

                return result.ToList();
            }
        }
    }
}