using ShoppingApp.Entities;

namespace ShoppingAPI.Configuration
{
    /// <summary>
    /// This class is responsible for the category configuration.
    /// </summary>
    public class CategoryConfiguration : EntityBaseConfiguration<CategoryEntity>
    {
        /// <summary>
        /// Initializes the category configuration.
        /// </summary>
        public CategoryConfiguration()
        {
            Property(a => a.Name).IsRequired().HasMaxLength(50);

            HasMany(a => a.SubCategories).WithRequired(b => b.Category).HasForeignKey(c => c.CategoryId);
        }
    }
}