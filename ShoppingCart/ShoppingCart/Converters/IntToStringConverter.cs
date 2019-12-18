using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Converters
{
    /// <summary>
    /// This class have methods to convert the integer values to string.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class IntToStringConverter : IValueConverter
    {
        /// <summary>
        /// This method is used to convert the integer to string.
        /// </summary>
        /// <param name="value">Gets the value</param>
        /// <param name="targetType">Gets the targetType</param>
        /// <param name="parameter">Gets the parameter</param>
        /// <param name="culture">Gets the culture</param>
        /// <returns>The string</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cardIcon = string.Empty;
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                var cardNumber = value.ToString().Substring(0, Math.Min(2, value.ToString().Length));
                if (cardNumber.Trim() == "54") return "Card.png";
            }

            return cardIcon;
        }

        /// <summary>
        /// This method is used to convert the string to integer.
        /// </summary>
        /// <param name="value">Gets the value</param>
        /// <param name="targetType">Gets the targetType</param>
        /// <param name="parameter">Gets the parameter</param>
        /// <param name="culture">Gets the culture</param>
        /// <returns>A boolean value</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}