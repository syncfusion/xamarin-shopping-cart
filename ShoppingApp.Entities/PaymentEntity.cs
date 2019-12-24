namespace ShoppingApp.Entities
{
    /// <summary>
    /// This class is responsible for the PaymentEntity.
    /// </summary>
    public class PaymentEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the payment mode.
        /// </summary>
        public string PaymentMode { get; set; }

        /// <summary>
        /// Gets or sets the card type icon.
        /// </summary>
        public string CardTypeIcon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted enabled  or not.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}