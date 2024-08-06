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
            var categoryDto = await _mediator.Send(new GetCategoriesQuery());

            //List<CategoryViewModel> objCategoryList = _unitOfWork.Category.GetAll().Select(x => new CategoryViewModel
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    DisplayOrder = x.DisplayOrder
            //}).ToList();
            return Ok(categoryDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var categoryDto = await _mediator.Send(new GetCategoryQuery { Id = id });
            return Ok(categoryDto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] CategoryDto categoryDto)
        {
            var result = _validator.Validate(categoryDto);

            if (result.IsValid)
            {
                var mappedCategory = _mapper.Map<AddCategoryCommand>(categoryDto);
                var categoryToAdd = _mediator.Send(mappedCategory);
                return Ok(categoryToAdd);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        public IActionResult Put(int id, [FromBody] CategoryDto categoryDto)
        {
            var result = _validator.Validate(categoryDto);
            if (id != categoryDto.Id)
            {
                return NotFound();
            }
            if (result.IsValid)
            {
                var mappedCategory = _mapper.Map<UpdateCategoryCommand>(categoryDto);
                var categoryToUpdate = _mediator.Send(mappedCategory);
                return Ok(categoryToUpdate);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDelete = _mediator.Send(new DeleteCategoryCommand { Id = id });
            return Ok(categoryDelete);
        }
    }
}
