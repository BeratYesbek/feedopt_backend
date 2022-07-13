using System.Threading.Tasks;
using Core.Entity.Concretes;
using Entity.Concretes;

namespace Business.Services.Abstracts
{
    public interface ITelegramService
    {
        Task SendNewPostAsync(Advert advert, User user);
    }
}
