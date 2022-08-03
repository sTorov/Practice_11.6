using System.Text.RegularExpressions;

namespace TelegramBot.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Удаление лишних пробелов из сообщения и замена символа '.' на ','
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveUnnecessary(this string str)
        {
            str = Regex.Replace(str, @"\s+", " ");

            if (str.Contains('.'))
                return str.Replace('.', ',');

            return str;
        }
    }
}
