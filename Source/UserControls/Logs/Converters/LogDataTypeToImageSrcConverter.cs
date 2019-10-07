using JsonFlier.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JsonFlier.UserControls.Logs.Converters
{
    public class LogDataTypeToImageSrcConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value?.ToString())
            {
                case "Text":
                    return "pack://siteoforigin:,,,/Resources/text.png";

                case "Exception":
                    return "pack://siteoforigin:,,,/Resources/exception.png";

                case "Object":
                    return "pack://siteoforigin:,,,/Resources/cube.png";

                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
