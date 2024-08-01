using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Staj.Dtos;
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

        public ProductController(ILogger<ProductController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
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
            if (ModelState.IsValid)
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
                return Ok(productVM);
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

            if (ModelState.IsValid)
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
                return Ok(productVM);
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