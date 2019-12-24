using ShoppingApp.Entities;

namespace ShoppingAPI.Configuration
{
    /// <summary>
    /// This class is responsible for the product configuration.
    /// </summary>
    public class ProductConfiguration : EntityBaseConfiguration<ProductEntity>
    {
        /// <summary>
        /// Initializes the product configuration.
        /// </summary>
        public ProductConfiguration()
        {
            Property(a => a.UserId).IsOptional();
            Property(a => a.SubCategoryId).IsRequired();
            HasMany(a => a.Reviews).WithOptional(b => b.Product).HasForeignKey(c => c.ProductId);
            HasMany(a => a.PreviewImages).WithRequired(b => b.Product).HasForeignKey(c => c.ProductId);
        }
    }
}