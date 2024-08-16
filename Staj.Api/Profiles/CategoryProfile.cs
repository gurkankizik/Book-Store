using AutoMapper;
using Staj.Api.Dtos;
using Staj.Api.Features.Categories.Commands.AddCategory;
using Staj.Api.Features.Categories.Commands.UpdateCategory;
using Staj.Api.Features.Categories.Queries.GetCategories;
using Staj.Api.Features.Categories.Queries.GetCategory;
using StajWeb.Models;

namespace Staj.Api.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, GetCategoriesResponse>();
            CreateMap<Category, GetCategoryResponse>();
            CreateMap<Category, AddCategoryResponse>().ReverseMap();
            CreateMap<AddCategoryCommand, Category>();
            CreateMap<Category, UpdateCategoryResponse>().ReverseMap();
            CreateMap<UpdateCategoryCommand, Category>();
        }
    }
}
