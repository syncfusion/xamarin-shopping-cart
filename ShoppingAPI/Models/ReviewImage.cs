namespace ShoppingAPI.Models
{
    /// <summary>
    /// This class is represent the review images data.
    /// </summary>
    public class ReviewImage
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the IsDeleted value.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the review Id.
        /// </summary>
        public int ReviewId { get; set; }

        /// <summary>
        /// Gets or sets the review enetity.
        /// </summary>
        public virtual Review ReviewEntity { get; set; }
    }
}