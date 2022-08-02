using System.Text.RegularExpressions;

namespace TelegramBot.Extensions
{
    public static class StringExtension
    {
        public static string RemoveUnnecessary(this string str)
        {
            str = Regex.Replace(str, @"\s+", " ");

            if (str.Contains('.'))
                return str.Replace('.', ',');

            return str;
        }
    }
}
