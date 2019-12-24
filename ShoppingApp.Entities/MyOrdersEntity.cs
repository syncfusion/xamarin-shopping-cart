using System;
namespace ShoppingApp.Entities
{
    public class MyOrderEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the total quantity.
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// Gets or sets the added datetime.
        /// </summary>
        public DateTime AddedDateTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted enabled  or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the user details.
        /// </summary>
        public virtual UserEntity User { get; set; }

        /// <summary>
        /// Gets or sets the product details.
        /// </summary>
        public virtual ProductEntity Product { get; set; }
    }
}
