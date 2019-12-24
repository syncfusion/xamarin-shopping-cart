using System;

namespace ShoppingAPI.Areas.HelpPage
{
    /// <summary>
    /// This represents an invalid sample on the help page. There's a display template named InvalidSample associated with
    /// this class.
    /// </summary>
    public class InvalidSample
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSample" /> class.
        /// </summary>
        /// <param name="errorMessage"></param>
        public InvalidSample(string errorMessage)
        {
            if (errorMessage == null) throw new ArgumentNullException("errorMessage");
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets the error message value.
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Check whether object is equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as InvalidSample;
            return other != null && ErrorMessage == other.ErrorMessage;
        }

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ErrorMessage.GetHashCode();
        }

        /// <summary>
        /// Get the string value.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}