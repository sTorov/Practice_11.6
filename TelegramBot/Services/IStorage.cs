using TelegramBot.Models;

namespace TelegramBot.Services
{
    /// <summary>
    /// Описание манипуляций над хранящимися данными
    /// </summary>
    interface IStorage
    {
        /// <summary>
        /// Получение текущей сессии по ID чата
        /// </summary>
        /// <param name="charId"></param>
        /// <returns></returns>
        Session GetSession(long charId);
    }
}
