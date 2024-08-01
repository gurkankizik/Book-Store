using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Staj.Dtos;
using StajWeb.DataAccess.Repository.IRepository;
using StajWeb.Models;

namespace Staj.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork UnitOfWork, IMapper mapper)
        {
            _unitOfWork = UnitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var categoryDtos = _unitOfWork.Category.GetAll();
            var data = _mapper.Map<List<CategoryDto>>(categoryDtos);

            //List<CategoryViewModel> objCategoryList = _unitOfWork.Category.GetAll().Select(x => new CategoryViewModel
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    DisplayOrder = x.DisplayOrder
            //}).ToList();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var categoryDto = _unitOfWork.Category.Get(u => u.Id == id);
            var data = _mapper.Map<CategoryDto>(categoryDto);
            if (categoryDto == null)
            {
                return NotFound();
            }
            return Ok(categoryDto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                var data = _mapper.Map<Category>(categoryDto);
                _unitOfWork.Category.Add(data);
                _unitOfWork.Save();
                return Ok(data);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        public IActionResult Put(int id, [FromBody] CategoryDto categoryDto)
        {
            var data = _mapper.Map<Category>(categoryDto);
            if (id != categoryDto.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(data);
                _unitOfWork.Save();
                return Ok(data);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        public IActionResult Delete(int id)
        {
            var categoryDto = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryDto == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(categoryDto);
            _unitOfWork.Save();
            return Ok(categoryDto);
        }
    }
}
