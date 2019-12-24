using System;
using System.Collections.Generic;

namespace ShoppingApp.Entities
{
    /// <summary>
    /// This class is responsible for the ProductEntity.
    /// </summary>
    public class ProductEntity
    {
        /// <summary>
        /// Initializes the ProductEntity.
        /// </summary>
        public ProductEntity()
        {
            Reviews = new List<ReviewEntity>();
            PreviewImages = new List<PreviewImageEntity>();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the actual price.
        /// </summary>
        public double ActualPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount price.
        /// </summary>
        public double DiscountPercent { get; set; }

        /// <summary>
        /// Gets or sets the overall rating.
        /// </summary>
        public double OverallRating { get; set; }

        /// <summary>
        /// Gets or sets the total quantity.
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// Gets or sets the subcategory id.
        /// </summary>
        public int SubCategoryId { get; set; }

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
        /// Gets or sets the user id.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user details.
        /// </summary>
        public virtual UserEntity User { get; set; }

        /// <summary>
        /// Gets or sets the subcategory.
        /// </summary>
        public virtual SubCategoryEntity SubCategory { get; set; }

        /// <summary>
        /// Gets or sets the reviews.
        /// </summary>
        public virtual ICollection<ReviewEntity> Reviews { get; set; }

        /// <summary>
        /// Gets or sets the preview images.
        /// </summary>
        public virtual ICollection<PreviewImageEntity> PreviewImages { get; set; }
    }
}