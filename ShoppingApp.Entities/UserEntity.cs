using System;
using System.Collections.Generic;

namespace ShoppingApp.Entities
{
    /// <summary>
    /// This class is responsible for the UserEntity.
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// Initializes the UserEntity.
        /// </summary>
        public UserEntity()
        {
            Products = new List<ProductEntity>();
            Address = new List<AddressEntity>();
            UserCarts = new List<UserCartEntity>();
            UserWishlists = new List<UserWishlistEntity>();
            Reviews = new List<ReviewEntity>();
            UserCards = new List<UserCardEntity>();
            Recents = new List<RecentEntity>();
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
        /// Gets or sets the email id.
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the salt.
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted enabled  or not.
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
        /// Gets or sets the forgot password.
        /// </summary>
        public string ForgotPassword { get; set; }

        /// <summary>
        /// Gets or sets the user card details.
        /// </summary>
        public virtual ICollection<UserCardEntity> UserCards { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        public virtual ICollection<ProductEntity> Products { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public virtual ICollection<AddressEntity> Address { get; set; }

        /// <summary>
        /// Gets or sets the user carts details.
        /// </summary>
        public virtual ICollection<UserCartEntity> UserCarts { get; set; }

        /// <summary>
        /// Get or sets the user wishlist details.
        /// </summary>
        public virtual ICollection<UserWishlistEntity> UserWishlists { get; set; }

        /// <summary>
        /// Gets or sets the reviews.
        /// </summary>
        public virtual ICollection<ReviewEntity> Reviews { get; set; }

        /// <summary>
        /// Gets or sets the recent items.
        /// </summary>
        public virtual ICollection<RecentEntity> Recents { get; set; }
    }
}