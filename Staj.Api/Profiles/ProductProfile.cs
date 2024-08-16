using AutoMapper;
using Staj.Api.Dtos;
using Staj.Api.Features.Products.Commands.AddProduct;
using Staj.Api.Features.Products.Commands.UpdateProduct;
using Staj.Api.Features.Products.Queries.GetProduct;
using Staj.Api.Features.Products.Queries.GetProducts;
using StajWeb.Models;

namespace Staj.Api.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, GetProductsResponse>().ReverseMap();
            CreateMap<Product, GetProductResponse>().ReverseMap();
            CreateMap<AddProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();
            CreateMap<AddProductResponse, Product>().ReverseMap();
            CreateMap<UpdateProductResponse, Product>().ReverseMap();
            CreateMap<AddProductCommand, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore()); // Ignore Category to set it manually
            CreateMap<Product, GetProductsResponse>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore()); // Ignore Category to set it manually
            CreateMap<Product, GetProductResponse>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        }
    }
}
