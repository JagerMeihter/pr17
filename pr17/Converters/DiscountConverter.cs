using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace pr17
{
    public class DiscountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal discount && discount > 15)
            {
                return new SolidColorBrush(Color.FromRgb(231, 76, 60)); // красный для скидки >15%
            }
            return new SolidColorBrush(Color.FromRgb(44, 62, 80)); // обычный фон
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}