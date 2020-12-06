using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JsonFlier.Converters
{
    public class VectorSourceConverter : IValueConverter
    {
        private ResourceDictionary Vectors;

        public VectorSourceConverter()
        {
            Vectors = new ResourceDictionary()
            {
                Source = new Uri("/JsonFlier;component/Images/Vectors.xaml", UriKind.Relative),
            };
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return null;

            string resourceKey = $"{value}Vector";

            if (Vectors.Contains(resourceKey))
            {
                return Vectors[resourceKey];
            }
            else
            {
                return Vectors["CubeVector"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
