namespace ShoppingAPI.Models
{
    /// <summary>
    /// This class is responsible for the user wishlist data.
    /// </summary>
    public class UserWishlist
    {
        /// <summary>
        /// Gets or sets the id .
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
        /// Gets or sets the is favorite value.
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public virtual Product Product { get; set; }
    }
}