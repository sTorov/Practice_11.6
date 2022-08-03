using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using TelegramBot.Controllers;
using TelegramBot.Services;
using TelegramBot.Configuration;

namespace TelegramBot
{
    /// <summary>
    /// Основной класс программы
    /// </summary>
    class Program
    {
        /// <summary>
        /// Основной метод программы
        /// </summary>
        /// <returns></returns>
        public static async Task Main()
        {
            var host = new HostBuilder().ConfigureServices(services => Configuration(services)).UseConsoleLifetime().Build();

            Console.WriteLine("Сервер запущен");
            
            await host.RunAsync();
            Console.WriteLine("Сервер остановлен");
        }

        /// <summary>
        /// Конфигурации для создания сервера
        /// </summary>
        /// <param name="services"></param>
        static void Configuration(IServiceCollection services)
        {
            var appSettings = BuildAppSettings();
            
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<IAction, Calculation>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken!));
            services.AddSingleton<IStorage, MemoryStorage>();

            services.AddHostedService<Bot>();
        }

        /// <summary>
        /// Получение конфигураций бота
        /// </summary>
        /// <returns></returns>
        static AppSettings BuildAppSettings()
        {
            return new AppSettings { BotToken = "5540655924:AAEyPVVUCv57Zm377Y6-mKzd0nTKvcCZORA" };
        }
    }
}