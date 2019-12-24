using ShoppingApp.Entities;

namespace ShoppingAPI.Configuration
{
    /// <summary>
    /// This class is responsible for the preview image configuration.
    /// </summary>
    public class PreviewImageConfiguration : EntityBaseConfiguration<PreviewImageEntity>
    {
        /// <summary>
        /// Initializes the preview image configuration.
        /// </summary>
        public PreviewImageConfiguration()
        {
            Property(a => a.ProductId).IsRequired();
        }
    }
}