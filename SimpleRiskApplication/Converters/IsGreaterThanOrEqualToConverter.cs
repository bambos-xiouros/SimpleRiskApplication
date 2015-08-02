using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpleRiskApplication.Converters
{
    public class IsGreaterThanOrEqualToConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new IsGreaterThanOrEqualToConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var doubleValue = double.Parse(value.ToString());
            var compareToValue = double.Parse(parameter.ToString());
            return doubleValue >= compareToValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}