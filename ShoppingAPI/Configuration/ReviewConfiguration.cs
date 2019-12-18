using ShoppingApp.Entities;

namespace ShoppingAPI.Configuration
{
    /// <summary>
    /// This class is responsible for the review configuration.
    /// </summary>
    public class ReviewConfiguration : EntityBaseConfiguration<ReviewEntity>
    {
        /// <summary>
        /// Initializes the review configuration.
        /// </summary>
        public ReviewConfiguration()
        {
            Property(a => a.UserId).IsOptional();
            Property(a => a.ProductId).IsOptional();
            HasMany(a => a.ReviewImages).WithOptional(b => b.Review).HasForeignKey(c => c.ReviewId);
        }
    }
}