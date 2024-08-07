using AutoMapper;
using Staj.Api.Dtos;
using Staj.Api.Features.Categories.Commands.AddCategory;
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
        {            // Existing mappings
            CreateMap<Product, ProductDto>();
            CreateMap<Product, GetProductsResponse>().ReverseMap();
            CreateMap<Product, GetProductResponse>().ReverseMap();
            CreateMap<ProductDto, Product>();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<AddProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();
            CreateMap<AddProductResponse, Product>().ReverseMap();
            CreateMap<UpdateProductResponse, Product>().ReverseMap();

            CreateMap<AddProductCommand, ProductDto>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductDto>().ReverseMap();
            CreateMap<AddProductResponse, ProductDto>().ReverseMap();
            CreateMap<UpdateProductResponse, ProductDto>().ReverseMap();

            // New mappings from response to DTO
            CreateMap<GetProductsResponse, ProductDto>().ReverseMap();
            CreateMap<GetProductResponse, ProductDto>().ReverseMap();
            CreateMap<AddProductResponse, ProductDto>().ReverseMap();
            CreateMap<UpdateProductResponse, ProductDto>().ReverseMap();

            CreateMap<Product, ProductDto>();
            CreateMap<Product, GetProductsResponse>();
            CreateMap<Product, GetProductResponse>();
            CreateMap<ProductDto, Product>();

            CreateMap<AddCategoryCommand, Category>();
            CreateMap<Category, AddCategoryResponse>();
            CreateMap<AddProductCommand, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore()); // Ignore Category to set it manually
            CreateMap<Product, GetProductsResponse>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<AddCategoryCommand, Category>();
            CreateMap<Category, AddCategoryResponse>();
            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore()); // Ignore Category to set it manually
            CreateMap<Product, GetProductsResponse>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<Product, GetProductResponse>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

        }
    }
}
