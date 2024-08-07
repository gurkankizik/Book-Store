using AutoMapper;
using FluentValidation;
using MediatR;
using Staj.Api.Features.Products.Queries.GetProducts;
using StajWeb.DataAccess.Repository.IRepository;
using StajWeb.Models;

namespace Staj.Api.Features.Products.Commands.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, AddProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetProductsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<AddProductCommand> _validator;

        public AddProductCommandHandler(IUnitOfWork unitOfWork, ILogger<GetProductsQueryHandler> logger, IMapper mapper, IValidator<AddProductCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        public Task<AddProductResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);

            if (result.IsValid)
            {
                var product = _mapper.Map<Product>(request);
                var category = _unitOfWork.Category.Get(u => u.Id == request.CategoryId);
                if (category == null)
                {
                    throw new ValidationException("Invalid CategoryId");
                }
                product.Category = category;
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                var response = _mapper.Map<AddProductResponse>(product);
                return Task.FromResult(response);
            }
            throw new ValidationException(result.Errors);

        }
    }
}
