namespace ShoppingAPI.Models
{
    /// <summary>
    /// This class is responsible for the status.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Gets or sets the whether IsSuccess is enabled or not.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }
    }
}