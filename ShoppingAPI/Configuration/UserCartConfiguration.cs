using ShoppingApp.Entities;

namespace ShoppingAPI.Configuration
{
    /// <summary>
    /// This class is responsible for the user cart configuration.
    /// </summary>
    public class UserCartConfiguration : EntityBaseConfiguration<UserCartEntity>
    {
        /// <summary>
        /// Initializes the user card configuration.
        /// </summary>
        public UserCartConfiguration()
        {
            Property(a => a.UserId).IsOptional();
            Property(a => a.ProductId).IsOptional();
        }
    }
}