namespace ShoppingApp.Entities
{
    /// <summary>
    /// This class is responsible for the UserWishlistEntity.
    /// </summary>
    public class UserWishlistEntity
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
        /// Gets or sets a value indicating whether IsFavorite enabled  or not.
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Gets or sets the user details
        /// </summary>
        public virtual UserEntity User { get; set; }

        /// <summary>
        /// Gets or sets the product details.
        /// </summary>
        public virtual ProductEntity Product { get; set; }
    }
}