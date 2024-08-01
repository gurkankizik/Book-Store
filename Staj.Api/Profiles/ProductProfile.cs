using AutoMapper;
using Staj.Dtos;
using StajWeb.Models;

namespace Staj.Api.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
