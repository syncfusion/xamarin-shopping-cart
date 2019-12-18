using AutoMapper;

namespace ShoppingAPI.Mappings
{
    /// <summary>
    /// This class is responsible for the mapper configuration.
    /// </summary>
    public class MapperConfig
    {
        /// <summary>
        /// Configure the profiles.
        /// </summary>
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DomainToModelMappingProfile>();
                cfg.AddProfile<ModelToDomainMappingProfile>();
            });
        }
    }
}