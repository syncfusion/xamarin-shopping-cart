using System.Collections.ObjectModel;

namespace ShoppingAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// This class is responsible for ComplexTypeModelDescription.
    /// </summary>
    public class ComplexTypeModelDescription : ModelDescription
    {
        /// <summary>
        /// Initialize the complex type model description.
        /// </summary>
        public ComplexTypeModelDescription()
        {
            Properties = new Collection<ParameterDescription>();
        }

        /// <summary>
        /// Gets the paramer description collection.
        /// </summary>
        public Collection<ParameterDescription> Properties { get; }
    }
}