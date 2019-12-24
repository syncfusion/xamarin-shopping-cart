using System;

namespace ShoppingApp.Entities
{
    /// <summary>
    /// This class is responsible for the PreviewImageEntity.
    /// </summary>
    public class PreviewImageEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or seys the product id.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the preview image.
        /// </summary>
        public string PreviewImage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted enabled or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the product details.
        /// </summary>
        public virtual ProductEntity Product { get; set; }
    }
}