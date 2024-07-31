using Microsoft.AspNetCore.Mvc;
using Staj.Model;
using StajWeb.DataAccess.Repository.IRepository;
using StajWeb.Models;

namespace Staj.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryViewModel>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            List<CategoryViewModel> objCategoryList = _unitOfWork.Category.GetAll().Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                DisplayOrder = x.DisplayOrder
            }).ToList();
            return Ok(objCategoryList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            Category category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                return Ok(category);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        public IActionResult Put(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                return Ok(category);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        public IActionResult Delete(int id)
        {
            Category category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            return Ok(category);
        }
    }
}
