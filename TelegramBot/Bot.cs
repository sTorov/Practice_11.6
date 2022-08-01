using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace TelegramBot
{
    class Bot : BackgroundService
    {
        private ITelegramBotClient _botClient;

        public Bot(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            
        }
    }
}
