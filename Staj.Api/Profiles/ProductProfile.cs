using AutoMapper;
using Staj.Api.Dtos;
using Staj.Api.Features.Products.Queries.GetProduct;
using Staj.Api.Features.Products.Queries.GetProducts;
using StajWeb.Models;
using StajWeb.Models.ViewModels;

namespace Staj.Api.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, GetProductsResponse>();
            CreateMap<Product, GetProductResponse>();
            CreateMap<ProductDto, Product>();

            CreateMap<ProductVM, Product>()
               .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id if it's auto-generated
                .ForMember(dest => dest.Category, opt => opt.Ignore()); // Ignore Category if it's a navigation property

            CreateMap<Product, ProductVM>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.CategoryList, opt => opt.Ignore()); // Ignore CategoryList as it's not part of Product
        }
    }
}
