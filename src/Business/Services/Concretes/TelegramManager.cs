
using System.Threading.Tasks;
using Business.Services.Abstracts;
using Entity.Concretes;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Business.Services.Concretes
{
    public class TelegramManager : ITelegramService
    {
        private readonly TelegramBotClient _botClient;
        private readonly IConfiguration _configuration;

        public TelegramManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _botClient = new TelegramBotClient(_configuration.GetSection("TelegramOptions").GetSection("TelegramToken").Value);
        }

        public async Task SendNewPostAsync(Advert advert, Core.Entity.User user)
        {
            await _botClient.SendTextMessageAsync(
                new ChatId(_configuration.GetSection("TelegramOptions")["TelegramChatId"]),
                $" Whoops! someone shared new post\n \n ### User Information ### \n email: {user.Email} \n fullname: {user.FullName} \n phone: {user.PhoneNumber}\n \n \n ### Advert Information ###\n Animal Name: {advert.AnimalName} \n Status: {advert.Status} \n CreatedAt: {advert.CreatedAt}");
        }
    }
}