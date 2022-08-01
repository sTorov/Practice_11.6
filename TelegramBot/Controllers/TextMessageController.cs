using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Services;

namespace TelegramBot.Controllers
{
    class TextMessageController
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient botClient, IStorage memoryStorage)
        {
            _botClient = botClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handler(Message message, CancellationToken token)
        {
            string func = _memoryStorage.GetSession(message.Chat.Id).Function;
            
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
                        $"\nНажмите одну из кнопок для выбора необходимой манипуляции.\n",
                        cancellationToken: token,
                        parseMode: ParseMode.Html,
                        replyMarkup: new InlineKeyboardMarkup(buttons)
                        );

                    if(func != String.Empty)
                    {
                        switch (func)
                        {
                            case "sum":
                                await _botClient.SendTextMessageAsync(message.Chat.Id, $"Сейчас выбрана функция:\n" + $"\n<b>Подсчёт суммы чисел</b>", cancellationToken: token, parseMode: ParseMode.Html).ConfigureAwait(false);
                                break;
                            case "str_length":
                                await _botClient.SendTextMessageAsync(message.Chat.Id, $"Сейчас выбрана функция:\n" + $"\n<b>Подсчёт количества символов</b>", cancellationToken: token, parseMode: ParseMode.Html).ConfigureAwait(false);
                                break;
                        }
                    }
                    return;

                default:
                    switch (func)
                    {
                        case "sum":
                            await _botClient.SendTextMessageAsync(message.Chat.Id, $"Сумма.", cancellationToken: token);
                            break;
                        case "str_length":
                            await _botClient.SendTextMessageAsync(message.Chat.Id, $"Символы.", cancellationToken: token);
                            break;
                        default:
                            await _botClient.SendTextMessageAsync(message.Chat.Id, $"Выберите функцию в главном меню.", cancellationToken: token);
                            break;
                    }
                    return;
            }            
        }
    }
}
