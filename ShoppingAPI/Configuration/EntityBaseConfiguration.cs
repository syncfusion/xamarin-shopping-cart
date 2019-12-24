using System.Data.Entity.ModelConfiguration;

namespace ShoppingAPI.Configuration
{
    /// <summary>
    /// This class is responsible for the entity base configuration.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityBaseConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
    }
}