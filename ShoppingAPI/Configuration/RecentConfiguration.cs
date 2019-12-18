using ShoppingApp.Entities;

namespace ShoppingAPI.Configuration
{
    /// <summary>
    /// This class is responsible for the recent configuration.
    /// </summary>
    public class RecentConfiguration : EntityBaseConfiguration<RecentEntity>
    {
        /// <summary>
        /// Initializes the recent configuration.
        /// </summary>
        public RecentConfiguration()
        {
            Property(a => a.UserId).IsOptional();
            Property(a => a.ProductId).IsOptional();
        }
    }
}