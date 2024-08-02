using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Staj.Api.Dtos;
using StajWeb.DataAccess.Repository.IRepository;
using StajWeb.Models;
using StajWeb.Models.ViewModels;

namespace Staj.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductVM> _validator;

        public ProductController(ILogger<ProductController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IMapper mapper, IValidator<ProductVM> validator)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var productDtos = _unitOfWork.Product.GetAll();
            var data = _mapper.Map<List<ProductDto>>(productDtos);
            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var productDtos = _unitOfWork.Category.Get(u => u.Id == id);
            var data = _mapper.Map<ProductDto>(productDtos);
            if (productDtos == null)
            {
                return NotFound();
            }
            return Ok(productDtos);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] ProductVM productVM)
        {
            var result = _validator.Validate(productVM);
            if (result.IsValid)
            {
                var product = _mapper.Map<Product>(productVM.Product);
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                return Ok(productVM);
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                //return Ok(productVM);
                return BadRequest(result.Errors);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public IActionResult Put(int id, [FromBody] ProductVM productVM)
        {
            if (id != productVM.Product.Id)
            {
                return NotFound();
            }
            var result = _validator.Validate(productVM);
            if (result.IsValid)
            {
                var product = _mapper.Map<Product>(productVM.Product);
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                return Ok(productVM);
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                //return Ok(productVM);
                return BadRequest(result.Errors);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public IActionResult Delete(int id)
        {
            var productDtos = _unitOfWork.Product.Get(u => u.Id == id);
            if (productDtos == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(productDtos);
            _unitOfWork.Save();
            return Ok(productDtos);
        }
    }
}
