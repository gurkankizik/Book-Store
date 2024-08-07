using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Staj.Api.Dtos;
using Staj.Api.Features.Products.Commands.AddProduct;
using Staj.Api.Features.Products.Commands.Delete_Product;
using Staj.Api.Features.Products.Commands.UpdateProduct;
using Staj.Api.Features.Products.Queries.GetProduct;
using Staj.Api.Features.Products.Queries.GetProducts;
using StajWeb.DataAccess.Repository.IRepository;
using StajWeb.Models;
using StajWeb.Models.ViewModels;

namespace Staj.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductVM> _validator;

        public ProductController(ILogger<ProductController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IMapper mapper, IValidator<ProductVM> validator, IMediator mediator)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _validator = validator;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _mediator.Send(new GetProductQuery { Id = id });
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] ProductVM productVM)
        {
            var result = _validator.Validate(productVM);
            if (result.IsValid)
            {
                var productToAdd = _mediator.Send(new AddProductCommand { ProductVM = productVM }).Result;
                return Ok(productToAdd);
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
                var productToUpdate = _mediator.Send(new UpdateProductCommand { ProductVM = productVM }).Result;
                return Ok(productToUpdate);
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
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteProductCommand { Id = id });
            return NoContent();
        }
    }
}
