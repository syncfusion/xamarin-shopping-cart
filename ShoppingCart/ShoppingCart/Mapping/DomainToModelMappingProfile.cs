using System.Linq;
using AutoMapper;
using ShoppingApp.Entities;
using ShoppingCart.Models;

namespace ShoppingCart.Mapping
{
    public class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            CreateMap<UserEntity, User>();
            CreateMap<AddressEntity, Address>()
                .ForMember(s => s.Name, d => d.MapFrom(e => e.User != null ? e.User.Name : string.Empty));
            CreateMap<CategoryEntity, Category>();
            CreateMap<SubCategoryEntity, SubCategory>();
            CreateMap<ProductEntity, Product>()
                .ForMember(s => s.Category, d => d.MapFrom(e => e.SubCategory.Name))
                .ForMember(s => s.PreviewImage, d => d.MapFrom(e => e.PreviewImages.FirstOrDefault().PreviewImage))
                .ForMember(s => s.Reviews, d => d.MapFrom(e => e.Reviews));
            CreateMap<ReviewEntity, Review>()
                .ForMember(s => s.Images, d => d.MapFrom(e => e.ReviewImages));
            CreateMap<ReviewImageEntity, ReviewImage>()
                .ForMember(s => s.Image, d => d.MapFrom(e => e.ReviewImage));
            CreateMap<PreviewImageEntity, PreviewImage>()
                .ForMember(s => s.Image, d => d.MapFrom(e => e.PreviewImage));
            CreateMap<UserCardEntity, UserCard>();
            CreateMap<BannerEntity, Banner>();
            CreateMap<PaymentEntity, Payment>();
            CreateMap<UserCardEntity, UserCard>();
            CreateMap<UserCartEntity, UserCart>()
                .ForMember(s => s.Product, d => d.MapFrom(e => e.Product));
        }
    }
}