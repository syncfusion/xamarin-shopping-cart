using System;

namespace ShoppingApp.Entities
{
    /// <summary>
    /// This class is responsible for the BannerEntity.
    /// </summary>
    public class BannerEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the banner image.
        /// </summary>
        public string BannerImage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted enabled  or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}