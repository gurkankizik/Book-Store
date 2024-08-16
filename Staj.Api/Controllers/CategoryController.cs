using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Staj.Api.Dtos;
using Staj.Api.Features.Categories.Commands.AddCategory;
using Staj.Api.Features.Categories.Commands.DeleteCategory;
using Staj.Api.Features.Categories.Commands.UpdateCategory;
using Staj.Api.Features.Categories.Queries.GetCategories;
using Staj.Api.Features.Categories.Queries.GetCategory;
using StajWeb.DataAccess.Repository.IRepository;
using StajWeb.Models;

namespace Staj.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryDto> _validator;

        public CategoryController(IUnitOfWork UnitOfWork, IMapper mapper, IValidator<CategoryDto> validator, IMediator mediator)
        {
            _unitOfWork = UnitOfWork;
            _mapper = mapper;
            _validator = validator;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var categories = await _mediator.Send(new GetCategoriesQuery());

            //List<CategoryViewModel> objCategoryList = _unitOfWork.Category.GetAll().Select(x => new CategoryViewModel
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    DisplayOrder = x.DisplayOrder
            //}).ToList();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _mediator.Send(new GetCategoryQuery { Id = id });
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] AddCategoryCommand command)
        {
            var categoryToAdd = await _mediator.Send(command);
            return Ok(categoryToAdd);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCategoryCommand command)
        {
            var categoryToUpdate = await _mediator.Send(command);
            return Ok(categoryToUpdate);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand { Id = id });
            return NoContent();
        }
    }
}
