using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;

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
            _botClient.StartReceiving(HandlerUpdateAsync, HandlerErrorAsync, new ReceiverOptions { AllowedUpdates = { } }, cancellationToken: token);

            Console.WriteLine("Бот запущен");
        }

        async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if(update.Type == UpdateType.CallbackQuery)
            {
                await _botClient.SendTextMessageAsync(update.CallbackQuery!.From.Id, $"Нажата кнопка", cancellationToken: token);
                return;
            }

            if(update.Type == UpdateType.Message)
            {
                await _botClient.SendTextMessageAsync(update.Message!.Chat.Id, $"Получено сообщение", cancellationToken: token);
                return;
            }
        }

        Task HandlerErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            var error = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API error\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
                _ => $"Unknown error\n{exception.GetType().Name}\n{exception.Message}"
            };

            Console.WriteLine(error);

            Console.WriteLine("10 секунд до повторного подключения...");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }
    }
}
