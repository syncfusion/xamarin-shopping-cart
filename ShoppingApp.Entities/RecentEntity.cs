using System;

namespace ShoppingApp.Entities
{
    /// <summary>
    /// This class is responsible for the RecentEntity.
    /// </summary>
    public class RecentEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the viewed date.
        /// </summary>
        public DateTime ViewedDate { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual UserEntity User { get; set; }

        /// <summary>
        /// Gets or sets the product details.
        /// </summary>
        public virtual ProductEntity Product { get; set; }
    }
}