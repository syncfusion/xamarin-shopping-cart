using System.Collections.ObjectModel;

namespace ShoppingAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// This class is responsible for ParameterDescription.
    /// </summary>
    public class ParameterDescription
    {
        /// <summary>
        /// initialize the parameter description.
        /// </summary>
        public ParameterDescription()
        {
            Annotations = new Collection<ParameterAnnotation>();
        }

        /// <summary>
        /// Gets the ParameterAnnotation collection.
        /// </summary>
        public Collection<ParameterAnnotation> Annotations { get; }

        /// <summary>
        /// Gets or sets the documentation.
        /// </summary>
        public string Documentation { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type description value.
        /// </summary>
        public ModelDescription TypeDescription { get; set; }
    }
}