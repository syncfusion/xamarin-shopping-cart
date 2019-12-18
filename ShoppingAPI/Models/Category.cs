using System.Collections.Generic;

namespace ShoppingAPI.Models
{
    /// <summary>
    /// This class is responsible for the category.
    /// </summary>
    public class Category
    {
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
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}