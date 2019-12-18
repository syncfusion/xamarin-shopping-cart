using AutoMapper;
using ShoppingAPI.Models;
using ShoppingApp.Entities;

namespace ShoppingAPI.Mappings
{
    /// <summary>
    /// This class is responsible for the DomainToModelMappingProfile.
    /// </summary>
    public class DomainToModelMappingProfile : Profile
    {
        /// <summary>
        /// Initializes the domain to model mapping profile.
        /// </summary>
        public DomainToModelMappingProfile()
        {
            CreateMap<UserEntity, User>();
            CreateMap<AddressEntity, Address>();
            CreateMap<CategoryEntity, Category>()
                .ForMember(s => s.ID, d => d.MapFrom(e => e.ID))
                .ForMember(s => s.Name, d => d.MapFrom(e => e.Name))
                .ForMember(s => s.Icon, d => d.MapFrom(e => e.Icon))
                .ForMember(s => s.SubCategories, d => d.MapFrom(e => e.SubCategories));
            CreateMap<SubCategoryEntity, SubCategory>();
            CreateMap<PaymentEntity, Payment>();
            CreateMap<PreviewImageEntity, PreviewImage>()
                .ForMember(s => s.Image, d => d.MapFrom(e => e.PreviewImage));
            CreateMap<ProductEntity, Product>();
            CreateMap<ReviewEntity, Review>();
            CreateMap<ReviewImageEntity, ReviewImage>()
                .ForMember(s => s.Image, d => d.MapFrom(e => e.ReviewImage));
            CreateMap<UserCartEntity, UserCart>();
            CreateMap<MyOrderEntity, MyOrder>();
            CreateMap<UserWishlistEntity, UserWishlist>();
        }
    }
}