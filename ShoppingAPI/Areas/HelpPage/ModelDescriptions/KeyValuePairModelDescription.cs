namespace ShoppingAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// This class is responsible for KeyValuePairModelDescription.
    /// </summary>
    public class KeyValuePairModelDescription : ModelDescription
    {
        /// <summary>
        /// Gets or sets the KeyModelDescription value.
        /// </summary>
        public ModelDescription KeyModelDescription { get; set; }

        /// <summary>
        /// Gets or sets the ValueModelDescription value.
        /// </summary>
        public ModelDescription ValueModelDescription { get; set; }
    }
}