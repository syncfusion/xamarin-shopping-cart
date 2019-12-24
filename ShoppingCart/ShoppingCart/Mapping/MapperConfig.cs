using AutoMapper;

namespace ShoppingCart.Mapping
{
    public class MapperConfig
    {
        public static void Config()
        {
            Mapper.Reset();
            Mapper.Initialize(config =>
            {
                config.AddProfile<DomainToModelMappingProfile>();
                config.AddProfile<ModelToDomainMappingProfile>();
            });
        }
    }
}