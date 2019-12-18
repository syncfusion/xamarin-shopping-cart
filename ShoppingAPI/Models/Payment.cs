namespace ShoppingAPI.Models
{
    /// <summary>
    /// This class is responsible for the payment.
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the transaction number.
        /// </summary>
        public string TransactionNumber { get; set; }

        /// <summary>
        /// Gets or sets the payment mode.
        /// </summary>
        public string PaymentMode { get; set; }

        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the cars type.
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Get or set the deleted value.
        /// </summary>
        public string IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the user Id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; }
    }
}