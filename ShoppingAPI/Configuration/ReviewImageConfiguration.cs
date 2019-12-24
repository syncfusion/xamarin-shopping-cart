using ShoppingApp.Entities;

namespace ShoppingAPI.Configuration
{
    /// <summary>
    /// This class is responsible for the review image configuration.
    /// </summary>
    public class ReviewImageConfiguration : EntityBaseConfiguration<ReviewImageEntity>
    {
        /// <summary>
        /// Initializes the review image configuration.
        /// </summary>
        public ReviewImageConfiguration()
        {
            Property(a => a.ReviewId).IsOptional();
        }
    }
}