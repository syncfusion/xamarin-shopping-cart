using System.Collections.ObjectModel;

namespace ShoppingAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// This class is responsible for dictionary model description.
    /// </summary>
    public class EnumTypeModelDescription : ModelDescription
    {
        /// <summary>
        /// Initializes the enum type model description.
        /// </summary>
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        /// <summary>
        /// Gets the EnumValueDescription collection.
        /// </summary>
        public Collection<EnumValueDescription> Values { get; }
    }
}