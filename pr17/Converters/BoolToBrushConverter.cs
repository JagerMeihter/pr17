using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace pr17.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isActive)
            {
                return isActive
                    ? new SolidColorBrush(Color.FromRgb(46, 204, 113))   // зелёный
                    : new SolidColorBrush(Color.FromRgb(231, 76, 60));   // красный
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}