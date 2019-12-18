using System;

namespace ShoppingAPI.Areas.HelpPage
{
    /// <summary>
    /// This represents a preformatted text sample on the help page. There's a display template named TextSample associated
    /// with this class.
    /// </summary>
    public class TextSample
    {
        /// <summary>
        /// Initializes the text sample.
        /// </summary>
        /// <param name="text"></param>
        public TextSample(string text)
        {
            if (text == null) throw new ArgumentNullException("text");
            Text = text;
        }

        /// <summary>
        /// Gets the text value.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Check whether the object is equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as TextSample;
            return other != null && Text == other.Text;
        }

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        /// <summary>
        /// Get the string value.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
    }
}