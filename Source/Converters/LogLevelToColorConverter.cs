using JsonFlier.Constants;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace JsonFlier.Converters
{
    public class LogLevelToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value?.ToString())
            {
                case JsonLoggerLogLevels.Critical:
                    return new SolidColorBrush(Color.FromRgb(0xff, 0xba, 0xba));

                case JsonLoggerLogLevels.Warning:
                    return new SolidColorBrush(Color.FromRgb(0xff, 0xe0, 0x97));

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
