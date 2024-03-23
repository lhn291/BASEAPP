using AutoMapper;
using BASEAPP.Models.DTOs.Cart;
using BASEAPP.Models.DTOs.Product;
using BASEAPP.Models.DTOs.ProductReview;
using BASEAPP.Models.DTOs.User;
using BASEAPP.Models.Models;

namespace BASEAPP.BaseAPI
{
    public static class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                // Product
                config.CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));

                config.CreateMap<ProductCreateDto, Product>().ReverseMap();
                config.CreateMap<ProductUpdateDto, Product>().ReverseMap();

                // ProductReview
                config.CreateMap<ProductReview, ProductReviewDto>()
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName)).ReverseMap();

                config.CreateMap<ProductReviewCreateDto, ProductReview>()
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                    .ForMember(dest => dest.ReviewDate, opt => opt.MapFrom(src => src.ReviewDate))
                    .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                    .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                    .IgnoreAllPropertiesWithAnInaccessibleSetter();

                // CartItem
                config.CreateMap<CartItem, CartItemDto>()
                    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice))
                    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                    .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Product.ImagePath))
                    .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.UnitPrice * src.Quantity));
            });

            return mappingConfig;
        }
    }
}
