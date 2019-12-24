using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Converters
{
    /// <summary>
    /// This class have methods to convert the bool values to visibility.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class VisibilityConverter : IValueConverter
    {
        /// <summary>
        /// This method is used to convert the bool to visibility.
        /// </summary>
        /// <param name="value">Gets the value</param>
        /// <param name="targetType">Gets the targetType</param>
        /// <param name="parameter">Gets the parameter</param>
        /// <param name="culture">Gets the culture</param>
        /// <returns>The visibility</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool) value;
        }

        /// <summary>
        /// This method is used to convert the visibility to boolean.
        /// </summary>
        /// <param name="value">Gets the value</param>
        /// <param name="targetType">Gets the targetType</param>
        /// <param name="parameter">Gets the parameter</param>
        /// <param name="culture">Gets the culture</param>
        /// <returns>A boolean value</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}