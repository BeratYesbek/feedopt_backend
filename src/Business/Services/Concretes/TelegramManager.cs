
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
            _botClient = new TelegramBotClient(_configuration.GetSection("TelegramOptions")["TelegramToken"]);
        }

        public async Task SendNewPostAsync(Advert advert, Core.Entity.User user)
        {
            await _botClient.SendTextMessageAsync(
                new ChatId(_configuration.GetSection("TelegramOptions")["TelegramChatId"]),
                $" Whoops! someone shared new post\n ### User Information\n email: {user.Email}" +
                $" fullname: {user.FullName} phone: {user.PhoneNumber}\n ### Advert Information\n" +
                $"Animal Name: ${advert.AnimalName}" +
                $"Status: ${advert.Status}" +
                $"CreatedAt: {advert.CreatedAt}");
        }
    }
}