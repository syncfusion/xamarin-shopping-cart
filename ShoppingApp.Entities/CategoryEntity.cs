using System.Collections.Generic;

namespace ShoppingApp.Entities
{
    /// <summary>
    /// This class is responsible for the CategoryEntity.
    /// </summary>
    public class CategoryEntity
    {
        /// <summary>
        /// Initializes the CategoryEntity.
        /// </summary>
        public CategoryEntity()
        {
            SubCategories = new List<SubCategoryEntity>();
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
        /// Gets or sets the icon.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the subcategories.
        /// </summary>
        public virtual ICollection<SubCategoryEntity> SubCategories { get; set; }
    }
}