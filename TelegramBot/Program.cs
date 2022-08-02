using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBot.Controllers;
using TelegramBot.Services;
using TelegramBot.Configuration;

namespace TelegramBot
{
    class Program
    {
        public static async Task Main()
        {
            var host = new HostBuilder().ConfigureServices(services => Configuration(services)).UseConsoleLifetime().Build();

            Console.WriteLine("Сервер запущен");
            
            await host.RunAsync();
            Console.WriteLine("Сервер остановлен");
        }

        static void Configuration(IServiceCollection services)
        {
            var appSettings = BuildAppSettings();
            
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<IAction, NumberAction>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken!));
            services.AddSingleton<IStorage, MemoryStorage>();

            services.AddHostedService<Bot>();
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings { BotToken = "5540655924:AAEyPVVUCv57Zm377Y6-mKzd0nTKvcCZORA" };
        }
    }
}