using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;
using Entity.Dtos;

namespace Business.Abstracts
{
    public interface IChatService
    {
        Task<IDataResult<Chat>> Add(Chat chat);
        Task<IDataResult<List<ChatDto>>> UpdateChatList(List<ChatDto> list);
        Task<IDataResult<Chat>> UpdateChat(Chat chat);
        Task<IDataResult<List<ChatDto>>> GetAllByReceiverIdAndSenderId(int senderId, int receiverId);
        Task<IDataResult<List<ChatDto>>> GetAllLastMessages(int id);
    }
}