namespace ShoppingApp.Entities
{
    /// <summary>
    /// This class is responsible for the ReviewImageEntity.
    /// </summary>
    public class ReviewImageEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the review images.
        /// </summary>
        public string ReviewImage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted enabled  or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the review id.
        /// </summary>
        public int ReviewId { get; set; }

        /// <summary>
        /// Gets or sets the review details.
        /// </summary>
        public virtual ReviewEntity Review { get; set; }
    }
}