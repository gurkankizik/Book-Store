using AutoMapper;
using Staj.Api.Dtos;
using StajWeb.Models;

namespace Staj.Api.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
