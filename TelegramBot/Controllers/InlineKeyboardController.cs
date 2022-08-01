using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Services;


namespace TelegramBot.Controllers
{
    internal class InlineKeyboardController
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IStorage _memoryStorage;

        public InlineKeyboardController(ITelegramBotClient botClient, IStorage memoryStorage)
        {
            _botClient = botClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handler(CallbackQuery? callbackQuery, CancellationToken token)
        {
            if(callbackQuery?.Data == null)
                return;

            string func = _memoryStorage.GetSession(callbackQuery.From.Id).Function;

            await _botClient.SendTextMessageAsync(callbackQuery.From.Id, func, cancellationToken: token);
            return;
        }
    }
}
