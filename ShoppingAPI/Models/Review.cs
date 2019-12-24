using System;
using System.Collections.Generic;

namespace ShoppingAPI.Models
{
    /// <summary>
    /// This class is responsible for the review data.
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the User Id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the product Id.
        /// </summary>
        public int ProductId { get; set; }

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
        /// Gets or sets the IsDeleted value.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        public virtual ICollection<ReviewImage> Images { get; set; }
    }
}