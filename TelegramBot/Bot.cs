using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using TelegramBot.Controllers;

namespace TelegramBot
{
    class Bot : BackgroundService
    {
        private ITelegramBotClient _botClient;
        private InlineKeyboardController _inlineKeyboardController;
        private TextMessageController _textMessageController;
        private DefaultMessageController _defaultMessageController;

        public Bot(ITelegramBotClient botClient, InlineKeyboardController inlineKeyboardController, TextMessageController textMessageController, DefaultMessageController defaultMessageController)
        {
            _botClient = botClient;
            _inlineKeyboardController = inlineKeyboardController;
            _textMessageController = textMessageController;
            _defaultMessageController = defaultMessageController;
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
                await _inlineKeyboardController.Handler(update.CallbackQuery, token);
                return;
            }

            if(update.Type == UpdateType.Message)
            {
                switch(update.Message!.Type)
                {
                    case MessageType.Text:
                        await _textMessageController.Handler(update.Message, token);
                        return;
                    default:
                        await _defaultMessageController.Handler(update.Message, token);
                        return;
                }               
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
