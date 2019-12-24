using ShoppingApp.Entities;

namespace ShoppingAPI.Configuration
{
    /// <summary>
    /// This class is responsible for the user configuration.
    /// </summary>
    public class UserConfiguration : EntityBaseConfiguration<UserEntity>
    {
        /// <summary>
        /// Initializes the user configuration.
        /// </summary>
        public UserConfiguration()
        {
            HasMany(a => a.UserCards).WithOptional(b => b.User).HasForeignKey(c => c.UserId);
            HasMany(a => a.Address).WithOptional(b => b.User).HasForeignKey(c => c.UserId);
            HasMany(a => a.Reviews).WithOptional(b => b.User).HasForeignKey(c => c.UserId);
            HasMany(a => a.Products).WithOptional(b => b.User).HasForeignKey(c => c.UserId);
            HasMany(a => a.UserWishlists).WithOptional(b => b.User).HasForeignKey(c => c.UserId);
            HasMany(a => a.UserCarts).WithOptional(b => b.User).HasForeignKey(c => c.UserId);
            HasMany(a => a.Recents).WithOptional(b => b.User).HasForeignKey(c => c.UserId);
        }
    }
}