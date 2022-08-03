using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Controllers
{
    /// <summary>
    /// Контроллер для незадействованных видов сообщений
    /// </summary>
    class DefaultMessageController
    {
        private readonly ITelegramBotClient _botClient;

        public DefaultMessageController(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        /// <summary>
        /// Обработчик незадействованых видов сообщений
        /// </summary>
        /// <param name="message"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task Handler(Message message, CancellationToken token)
        {
            await _botClient.SendTextMessageAsync(message.Chat.Id, $"Получено сообщение неподдерживаемого формата.", cancellationToken: token);
            return;
        }
    }
}
