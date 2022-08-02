using System.Text;
using System.Text.RegularExpressions;
using TelegramBot.Extensions;

namespace TelegramBot.Services
{
    class NumberAction : IAction
    {           
        private List<double> AddNumber(string value)
        {
            string newValue = value.RemoveUnnecessary();

            List<double> list = new();
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
                            return null!;

                        list.Add(result);
                        sb.Clear();
                    }
                    else
                        return null!;
                }
            }

            return list;
        }

        public string Sum(string value)
        {
            double sum = 0;

            var list = AddNumber(value);
            
            if (list == null)
                return "<b>Ошибка!</b>\nВведите числа через пробел!";
            if (list.Count == 1)
                return "<b>Ошибка!</b>\nВведите 2 или более значений!";

            foreach (double number in list)
                sum += number;

            return "<b>Сумма чисел: </b>" + Math.Round(sum, 6, MidpointRounding.AwayFromZero).ToString();
        }
    }
}
