using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpleRiskApplication.Converters
{
    public class StakeMultiplierConverter : IMultiValueConverter
    {
        public static readonly IMultiValueConverter Instance = new StakeMultiplierConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var stake = double.Parse(values[0].ToString());
            var averageStake = double.Parse(values[1].ToString());
            return (stake/averageStake).ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
