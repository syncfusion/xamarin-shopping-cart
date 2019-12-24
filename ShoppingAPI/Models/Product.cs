using System;
using System.Collections.Generic;

namespace ShoppingAPI.Models
{
    /// <summary>
    /// This class is responsible for the product.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the Id.
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
        /// Gets or sets the discount percent.
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
        /// Gets or sets the subcategory Id.
        /// </summary>
        public int SubCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the IsDeleted value.
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
        /// Gets or sets the subcategory.
        /// </summary>
        public virtual SubCategory SubCategory { get; set; }

        /// <summary>
        /// Gets or sets the review collection.
        /// </summary>
        public virtual List<Review> Reviews { get; set; }

        /// <summary>
        /// Gets or sets the preview images.
        /// </summary>
        public virtual List<PreviewImage> PreviewImages { get; set; }

        /// <summary>
        /// Gets or sets the user wishlist.
        /// </summary>
        public virtual List<UserWishlist> UserWishlists { get; set; }
    }
}