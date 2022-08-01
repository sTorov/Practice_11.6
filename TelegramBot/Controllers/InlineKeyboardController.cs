using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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

            _memoryStorage.GetSession(callbackQuery.From.Id).Function = callbackQuery.Data;

            string str = callbackQuery.Data switch
            {
                "sum" => $"Сложение чисел",
                "str_length" => $"Подсчёт количества символов",
                _ => string.Empty
            };

            string text = callbackQuery.Data switch
            {
                "sum" => $"2 или более числа через пробел.",
                "str_length" => $"текст для подсчёта символов.",
                _ => string.Empty
            };

            await _botClient.SendTextMessageAsync(
                callbackQuery.From.Id, 
                $"Выбранное действие: <b>{str}</b>\n" +  $"\nВведите {text}\n", 
                cancellationToken: token,
                parseMode: ParseMode.Html
                );
            return;
        }
    }
}
