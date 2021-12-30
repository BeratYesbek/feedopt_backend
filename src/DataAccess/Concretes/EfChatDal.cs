﻿using System;
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
                // purpose of this query if you want to get data from database by senderID and receiverID,firstly
                // it will control senderUser equals coming senderID and receiverUser equals coming receiverID
                // we can return correct data this is only way

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
            // purpose of this query if you would like to get data from database by datetime you must use this query 
            // firstly query will control who you are ? after it will control again four possibilities.
            // check senderUser => on users, check receiverUser on users 
            /*
             * ##### Example Data
             *  {
      "chat": {
        "id": 1,
        "senderId": 2,
        "receiverId": 1002,
        "message": "Hello",
        "date": "2021-10-18T13:23:13.765"
      },
      "senderUser": {
        "id": 2,
        "firstName": "berat",
        "lastName": "yesbek",
        "email": "berat@gmail.com"
      },
      "receiverUser": {
        "id": 1002,
        "firstName": "elmir",
        "lastName": "i",
        "email": "elmir@gmail.com"
      }
    },
             *
             */
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