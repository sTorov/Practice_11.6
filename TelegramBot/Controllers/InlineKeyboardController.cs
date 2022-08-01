using Telegram.Bot;
using Telegram.Bot.Types;


namespace TelegramBot.Controllers
{
    internal class InlineKeyboardController
    {
        private readonly ITelegramBotClient _botClient;

        public InlineKeyboardController(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task Handler(CallbackQuery? callbackQuery, CancellationToken token)
        {
            if(callbackQuery?.Data == null)
                return;

            await _botClient.SendTextMessageAsync(callbackQuery.From.Id, "Нажата кнопка", cancellationToken: token);
            return;
        }
    }
}
