using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Diary
{
    /// <summary>
    /// Класс для преобразование checkBox в null
    /// </summary>
    public class NullToFalseConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return value != null; // Если `null`, то `false` (CheckBox отключен)
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing; // Запрещаем обратное преобразование
        }
    }
}
