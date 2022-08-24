using System.Threading.Tasks;
using Business.Services.Abstracts;
using Entity.Concretes;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Business.Services.Concretes
{
    /// <summary>
    /// Telegram service
    /// </summary>
    public class TelegramManager : ITelegramService
    {
        private readonly TelegramBotClient _botClient;
        private readonly IConfiguration _configuration;

        public TelegramManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _botClient = new TelegramBotClient(_configuration.GetSection("TelegramOptions").GetSection("TelegramToken").Value);
        }

        /// <summary>
        /// Whenever someone share new post,this method notify thought TELEGRAM BOT. 
        /// </summary>
        /// <param name="advert"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task SendNewPostAsync(Advert advert, Core.Entity.Concretes.User user)
        {
            await _botClient.SendTextMessageAsync(
                new ChatId(_configuration.GetSection("TelegramOptions")["TelegramChatId"]),
                $" Whoops! someone shared new post\n \n ### User Information ### \n email: {user.Email} \n fullname: {user.FullName} \n \n \n \n ### Advert Information ###\n Animal Name: {advert.AnimalName} \n Status: {advert.Status} \n CreatedAt: {advert.CreatedAt}");
        }
    }
}