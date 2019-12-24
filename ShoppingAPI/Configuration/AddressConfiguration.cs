using ShoppingApp.Entities;

namespace ShoppingAPI.Configuration
{
    /// <summary>
    /// This class is responsible for the address configuration.
    /// </summary>
    public class AddressConfiguration : EntityBaseConfiguration<AddressEntity>
    {
        /// <summary>
        /// Initializes the AddressConfiguration.
        /// </summary>
        public AddressConfiguration()
        {
            Property(a => a.UserId).IsOptional();
        }
    }
}