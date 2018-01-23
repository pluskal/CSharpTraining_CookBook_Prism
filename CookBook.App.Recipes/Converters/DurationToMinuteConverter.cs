using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CookBook.App.Recipes.Converters
{
    public class DurationToMinuteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var duration = (TimeSpan?)value;
            return duration?.TotalMinutes;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}