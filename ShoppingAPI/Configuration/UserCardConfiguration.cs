using ShoppingApp.Entities;

namespace ShoppingAPI.Configuration
{
    /// <summary>
    /// This class is responsible for the user card configuration.
    /// </summary>
    public class UserCardConfiguration : EntityBaseConfiguration<UserCardEntity>
    {
        /// <summary>
        /// Initializes the usercard configuration.
        /// </summary>
        public UserCardConfiguration()
        {
            Property(a => a.UserId).IsOptional();
        }
    }
}