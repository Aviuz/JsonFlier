using System;
using System.Globalization;
using System.Windows.Data;

namespace JsonFlier.UIControls.LogTabs.JsonLog.JsonTreeView
{
    public class TreeViewCollapsedToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is true)
                return -90;
            else if (value is false)
                return 0;
            else
                throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
