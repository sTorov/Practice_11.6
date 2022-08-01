using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

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
                        $"\nНажмите одну из кнопок для выбора необходимой манипуляции\n",
                        cancellationToken: token,
                        parseMode: ParseMode.Html,
                        replyMarkup: new InlineKeyboardMarkup(buttons)
                        );
                    return;
                default:
                    await _botClient.SendTextMessageAsync(message.Chat.Id, $"Получено текстовое сообщение.", cancellationToken: token);
                    return;
            }

            
        }
    }
}
