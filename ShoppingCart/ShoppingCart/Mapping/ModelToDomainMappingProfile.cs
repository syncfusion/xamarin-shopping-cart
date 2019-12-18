using AutoMapper;
using ShoppingApp.Entities;
using ShoppingCart.Models;

namespace ShoppingCart.Mapping
{
    public class ModelToDomainMappingProfile : Profile
    {
        public ModelToDomainMappingProfile()
        {
            CreateMap<User, UserEntity>();
            CreateMap<Address, AddressEntity>();
            CreateMap<Category, CategoryEntity>();
            CreateMap<SubCategory, SubCategoryEntity>();
            CreateMap<Product, ProductEntity>();
            CreateMap<Review, ReviewEntity>()
                .ForMember(s => s.ReviewImages, d => d.MapFrom(e => e.Images));
            CreateMap<ReviewImage, ReviewImageEntity>()
                .ForMember(s => s.ReviewImage, d => d.MapFrom(e => e.Image));
            CreateMap<PreviewImage, PreviewImageEntity>()
                .ForMember(s => s.PreviewImage, d => d.MapFrom(e => e.Image));
            CreateMap<UserCard, UserCardEntity>();
            CreateMap<Banner, BannerEntity>();
            CreateMap<Payment, PaymentEntity>();
            CreateMap<UserCard, UserCardEntity>();
            CreateMap<UserCart, UserCartEntity>();
        }
    }
}