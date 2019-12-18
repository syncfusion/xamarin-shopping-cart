using ShoppingApp.Entities;

namespace ShoppingAPI.Configuration
{
    /// <summary>
    /// This class is responsible for the subcategory configuration.
    /// </summary>
    public class SubcategoryConfiguration : EntityBaseConfiguration<SubCategoryEntity>
    {
        /// <summary>
        /// Initializes the subcategory configuration.
        /// </summary>
        public SubcategoryConfiguration()
        {
            Property(a => a.CategoryId).IsRequired();
        }
    }
}