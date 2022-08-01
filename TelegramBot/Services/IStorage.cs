using TelegramBot.Models;

namespace TelegramBot.Services
{
    internal interface IStorage
    {
        Session GetSession(long charId);
    }
}
