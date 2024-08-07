using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Staj.Api.Dtos;
using Staj.Api.Features.Products.Commands.AddProduct;
using Staj.Api.Features.Products.Commands.Delete_Product;
using Staj.Api.Features.Products.Commands.UpdateProduct;
using Staj.Api.Features.Products.Queries.GetProduct;
using Staj.Api.Features.Products.Queries.GetProducts;
using StajWeb.DataAccess.Repository.IRepository;

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
        private readonly IValidator<ProductDto> _validator;

        public ProductController(ILogger<ProductController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IMapper mapper, IValidator<ProductDto> validator, IMediator mediator)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _validator = validator;

            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
        public async Task<IActionResult> Post([FromBody] AddProductCommand command)
        {
            var productToAdd = await _mediator.Send(command);
            return Ok(productToAdd);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProductCommand command)
        {
            var productToUpdate = await _mediator.Send(command);
            return Ok(productToUpdate);
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
