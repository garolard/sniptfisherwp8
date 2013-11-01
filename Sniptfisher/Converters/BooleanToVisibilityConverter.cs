using System.Windows.Data;
using System.Windows;

namespace Sniptfisher.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Visibility)
            {
                if ((Visibility)value == Visibility.Visible) return true; else return false;
            }
            else
            {
                return false;
            }
        }
    }
}
