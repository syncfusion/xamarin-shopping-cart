using ShoppingApp.Entities;

namespace ShoppingAPI.Configuration
{
    /// <summary>
    /// This class is responsible for the user wishlist configuration.
    /// </summary>
    public class UserWishlistConfiguration : EntityBaseConfiguration<UserWishlistEntity>
    {
        /// <summary>
        /// Initializes the user wishlist configuration.
        /// </summary>
        public UserWishlistConfiguration()
        {
            Property(a => a.UserId).IsOptional();
            Property(a => a.ProductId).IsOptional();
        }
    }
}