using System.Text;
using System.Text.RegularExpressions;
using TelegramBot.Extensions;

namespace TelegramBot.Services
{
    class Calculation : IAction
    {           
        /// <summary>
        /// Список чисел
        /// </summary>
        private List<double>? _listNumbers;

        public Calculation()
        {
            _listNumbers = new List<double>();
        }
        
        /// <summary>
        /// Получение списка чисел из сообщения
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private void GetNumbers(string value)
        {
            _listNumbers!.Clear();
            string newValue = value.RemoveUnnecessary();

            StringBuilder sb = new();
                        
            for (int i = 0; i < newValue.Length; i++)
            {
                if (newValue[i] != ' ' || i == newValue.Length - 1)
                    sb.Append(newValue[i]);

                if (newValue[i] == ' ' || i == newValue.Length - 1)
                {
                    if (double.TryParse(sb.ToString(), out double result))
                    {
                        if (Regex.IsMatch(sb.ToString(), @"\w*,+$"))
                        {
                            _listNumbers.Clear();
                            return;
                        }

                        _listNumbers.Add(result);
                        sb.Clear();
                    }
                    else
                    {
                        _listNumbers.Clear();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Сложение чисел, полученных из сообщения
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Action(string value)
        {
            double sum = 0;

            GetNumbers(value);
            
            if (_listNumbers!.Count == 0)
                return "<b>Ошибка!</b>\nВведите числа через пробел!";
            if (_listNumbers!.Count == 1)
                return "<b>Ошибка!</b>\nВведите 2 или более значений!";

            foreach (double number in _listNumbers)
                sum += number;

            return "<b>Сумма чисел: </b>" + Math.Round(sum, 6, MidpointRounding.AwayFromZero).ToString();
        }
    }
}
