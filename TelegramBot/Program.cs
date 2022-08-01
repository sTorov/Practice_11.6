using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

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
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("5540655924:AAEyPVVUCv57Zm377Y6-mKzd0nTKvcCZORA"));

            services.AddHostedService<Bot>();
        }
    }
}