using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Converters
{
    /// <summary>
    /// This class have methods to convert the int values to boolean.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class IntToBoolConverter : IValueConverter
    {
        /// <summary>
        /// This method is used to convert the int to bool.
        /// </summary>
        /// <param name="value">Gets the value.</param>
        /// <param name="targetType">Gets the target type.</param>
        /// <param name="parameter">Gets the parameter.</param>
        /// <param name="culture">Gets the culture.</param>
        /// <returns>Returns the bool.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int ItemCount;
            int.TryParse(value.ToString(), out ItemCount);
            if (ItemCount > 0)
                return true;
            return false;
        }

        /// <summary>
        /// This method is used to convert the bool to int.
        /// </summary>
        /// <param name="value">Gets the value.</param>
        /// <param name="targetType">Gets the target type.</param>
        /// <param name="parameter">Gets the parameter.</param>
        /// <param name="culture">Gets the culture.</param>
        /// <returns>Returns the int.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}