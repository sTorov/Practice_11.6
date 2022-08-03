namespace TelegramBot.Models
{
    /// <summary>
    /// Сессия пользовотеля
    /// </summary>
    class Session
    {
        /// <summary>
        /// Выбранное пользователем действие, производимое ботом
        /// </summary>
        public string? Function { get; set; }
    }
}
