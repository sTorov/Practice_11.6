namespace TelegramBot.Services
{
    /// <summary>
    /// Описание производимых действий над числами
    /// </summary>
    interface ICalculation
    {
        /// <summary>
        /// Сложение чисел
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Sum(string value);
    }
}
