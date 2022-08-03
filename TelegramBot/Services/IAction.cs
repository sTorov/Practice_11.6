namespace TelegramBot.Services
{
    /// <summary>
    /// Описание производимых действий различного характера
    /// </summary>
    interface IAction
    {
        /// <summary>
        /// Производимое действие
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Action(string value);
    }
}
