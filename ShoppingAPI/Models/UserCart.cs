using System;

namespace ShoppingAPI.Models
{
    /// <summary>
    /// This class is responsible for the user cart data.
    /// </summary>
    public class UserCart
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the added date time.
        /// </summary>
        public DateTime AddedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the IsDeleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}