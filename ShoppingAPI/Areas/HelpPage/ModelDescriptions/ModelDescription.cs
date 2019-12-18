using System;

namespace ShoppingAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Describes a type model.
    /// </summary>
    public abstract class ModelDescription
    {
        /// <summary>
        /// Gets or sets the Documentation value.
        /// </summary>
        public string Documentation { get; set; }

        /// <summary>
        /// Gets or sets the ModelType value.
        /// </summary>
        public Type ModelType { get; set; }

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public string Name { get; set; }
    }
}