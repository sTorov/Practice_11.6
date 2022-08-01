using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Controllers
{
    class DefaultMessageController
    {
        private readonly ITelegramBotClient _botClient;

        public DefaultMessageController(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task Handler(Message message, CancellationToken token)
        {

        }
    }
}
