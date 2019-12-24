using System;
using System.Collections.Generic;

namespace ShoppingApp.Entities
{
    /// <summary>
    /// This class is responsible for the ReviewEntity.
    /// </summary>
    public class ReviewEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the reviewed date.
        /// </summary>
        public DateTime ReviewedDate { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted enabled  or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user details.
        /// </summary>
        public virtual UserEntity User { get; set; }

        /// <summary>
        /// Gets or sets the product details.
        /// </summary>
        public virtual ProductEntity Product { get; set; }

        /// <summary>
        /// Gets or sets the review images.
        /// </summary>
        public virtual ICollection<ReviewImageEntity> ReviewImages { get; set; }
    }
}