using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ShoppingAPI.Configuration;
using ShoppingApp.Entities;

namespace ShoppingAPI.Database
{
    /// <summary>
    /// This class is responsible for the value controller.
    /// </summary>
    public class ShoppingContext : DbContext
    {
        /// <summary>
        /// Initializes the shopping context.
        /// </summary>
        public ShoppingContext() : base("name=Your DB")
        {
            Configuration.LazyLoadingEnabled = false;
            Database.Initialize(false);
        }

        /// <summary>
        /// Save the changes.
        /// </summary>
        public virtual void Commit()
        {
            base.SaveChanges();
        }

        /// <summary>
        /// On model creating.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ReviewConfiguration());
            modelBuilder.Configurations.Add(new ReviewImageConfiguration());
            modelBuilder.Configurations.Add(new SubcategoryConfiguration());
            modelBuilder.Configurations.Add(new UserCartConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserWishlistConfiguration());
            modelBuilder.Configurations.Add(new RecentConfiguration());
            modelBuilder.Configurations.Add(new MyOrderConfiguration());
        }

        #region Entity Sets

        /// <summary>
        /// Gets or set the user.
        /// </summary>
        public DbSet<UserEntity> User { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public DbSet<AddressEntity> Address { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public DbSet<CategoryEntity> Category { get; set; }

        /// <summary>
        /// Gets or sets the subcategory.
        /// </summary>
        public DbSet<SubCategoryEntity> SubCategory { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public DbSet<ProductEntity> Product { get; set; }

        /// <summary>
        /// Gets or sets the review.
        /// </summary>
        public DbSet<ReviewEntity> Review { get; set; }

        /// <summary>
        /// Gets or sets the review image.
        /// </summary>
        public DbSet<ReviewImageEntity> ReviewImage { get; set; }

        /// <summary>
        /// Gets or sets the preview image.
        /// </summary>
        public DbSet<PreviewImageEntity> PreviewImage { get; set; }

        /// <summary>
        /// Gets or sets the user wishlist.
        /// </summary>
        public DbSet<UserWishlistEntity> UserWishlist { get; set; }

        /// <summary>
        /// Gets or sets the user cart.
        /// </summary>
        public DbSet<UserCartEntity> UserCart { get; set; }

        /// <summary>
        /// Gets or sets the payment.
        /// </summary>
        public DbSet<PaymentEntity> Payment { get; set; }

        /// <summary>
        /// Gets or sets the user card.
        /// </summary>
        public DbSet<UserCardEntity> UserCard { get; set; }

        /// <summary>
        /// Gets or sets the recent product details.
        /// </summary>
        public DbSet<RecentEntity> Recent { get; set; }

        /// <summary>
        /// Gets or sets the banner.
        /// </summary>
        public DbSet<BannerEntity> Banner { get; set; }

        /// <summary>
        /// Gets or sets the banner.
        /// </summary>
        public DbSet<MyOrderEntity> MyOrder { get; set; }

        #endregion
    }
}