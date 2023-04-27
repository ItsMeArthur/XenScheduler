using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenScheduler.Converters
{
    public class MonthNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null)
                {
                    return null;
                }

                string text = (string)value;

                if (!string.IsNullOrEmpty(text))
                {
                    char[] chars = text.ToCharArray();
                    if (char.IsLetter(chars[0]))
                    {
                        chars[0] = char.ToUpper(chars[0]);
                    }
                    text = new string(chars);
                }

                return text;
            }
            catch
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
