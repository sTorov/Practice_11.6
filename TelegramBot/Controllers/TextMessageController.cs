using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Services;

namespace TelegramBot.Controllers
{
    /// <summary>
    /// Контроллер текстовых сообщений
    /// </summary>
    class TextMessageController
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IStorage _memoryStorage;
        private readonly ICalculation _calculation;

        public TextMessageController(ITelegramBotClient botClient, IStorage memoryStorage, ICalculation calculation)
        {
            _botClient = botClient;
            _memoryStorage = memoryStorage;
            _calculation = calculation;
        }

        /// <summary>
        /// Обработчик текстовых сообщений
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task Handler(Message message, CancellationToken token)
        {
            string func = _memoryStorage.GetSession(message.Chat.Id).Function!;
            
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Количество символов", "str_length"),
                        InlineKeyboardButton.WithCallbackData("Сумма чисел", "sum"),
                    });

                    await _botClient.SendTextMessageAsync(
                        message.Chat.Id,
                        $"<b> Этот бот может посчитать количество символов в строке, либо сумму введённых вами чисел.</b>\n" +
                        $"\nНажмите одну из кнопок для того чтобы узнать:\n",
                        cancellationToken: token,
                        parseMode: ParseMode.Html,
                        replyMarkup: new InlineKeyboardMarkup(buttons)
                        );                    
                    return;

                default:
                    switch (func)
                    {
                        case "sum":                            
                            await _botClient.SendTextMessageAsync(message.Chat.Id, _calculation.Sum(message.Text!), cancellationToken: token, parseMode: ParseMode.Html);
                            break;
                        case "str_length":
                            await _botClient.SendTextMessageAsync(message.Chat.Id, $"<b>Количество символов: </b>{message.Text!.Length}", cancellationToken: token, parseMode: ParseMode.Html);
                            break;
                        default:
                            await _botClient.SendTextMessageAsync(message.Chat.Id, $"<b>Ошибка!</b>\nВыберите действие в главном меню.\n\n/start - Главное меню", cancellationToken: token, parseMode: ParseMode.Html);
                            break;
                    }
                    return;
            }            
        }
    }
}
