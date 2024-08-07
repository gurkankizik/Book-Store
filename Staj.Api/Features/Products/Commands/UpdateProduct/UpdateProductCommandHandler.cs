using AutoMapper;
using FluentValidation;
using MediatR;
using Staj.Api.Features.Products.Queries.GetProducts;
using StajWeb.DataAccess.Repository.IRepository;

namespace Staj.Api.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetProductsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateProductCommand> _validator;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, ILogger<GetProductsQueryHandler> logger, IMapper mapper, IValidator<UpdateProductCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        public Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);

            if (result.IsValid)
            {
                var product = _unitOfWork.Product.Get(u => u.Id == request.Id);
                if (product == null)
                {
                    throw new ValidationException("Product not found");
                }

                _mapper.Map(request, product);

                var category = _unitOfWork.Category.Get(u => u.Id == request.CategoryId);
                if (category == null)
                {
                    throw new ValidationException("Invalid CategoryId");
                }
                product.Category = category;

                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();

                var response = _mapper.Map<UpdateProductResponse>(product);
                return Task.FromResult(response);
            }
            throw new ValidationException(result.Errors); ;
        }
    }
}
