using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Entity.Concretes;

namespace Business.Services.Abstracts
{
    public interface ITelegramService
    {
        Task SendNewPostAsync(Advert advert, User user);
    }
}
