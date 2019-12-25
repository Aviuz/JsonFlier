using JsonFlier.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace JsonFlier.UserControls.Logs.Converters
{
    public class LogTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value?.ToString())
            {
                case JsonLoggerLogLevels.Critical:
                    return new SolidColorBrush(Color.FromRgb(0xf5, 0xb8, 0xb8));

                case JsonLoggerLogLevels.Warning:
                    return new SolidColorBrush(Color.FromRgb(0xff, 0xf9, 0xc2));

                case JsonLoggerLogLevels.Info:
                    return new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0xff));

                case JsonLoggerLogLevels.Trace:
                    return new SolidColorBrush(Color.FromRgb(0xff, 0xff, 0xff));

                default:
                    return new SolidColorBrush(Colors.White);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
