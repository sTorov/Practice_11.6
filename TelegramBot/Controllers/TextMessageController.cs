using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Controllers
{
    class TextMessageController
    {
        private readonly ITelegramBotClient _botClient;

        public TextMessageController(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task Handler(Message message, CancellationToken token)
        {

        }
    }
}
