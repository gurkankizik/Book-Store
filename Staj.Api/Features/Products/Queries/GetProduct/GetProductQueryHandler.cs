using AutoMapper;
using MediatR;
using StajWeb.DataAccess.Repository.IRepository;

namespace Staj.Api.Features.Products.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetProductQueryHandler> _logger;
        private readonly IMapper _mapper;


        public GetProductQueryHandler(IUnitOfWork unitOfWork, ILogger<GetProductQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }


        public Task<GetProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = _unitOfWork.Product.Get(u => u.Id == request.Id, includeProperties: "Category");
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {request.Id} not found.");
                throw new KeyNotFoundException($"Product with ID {request.Id} not found.");
            }
            // Ensure the category is included
            if (product.Category == null)
            {
                product.Category = _unitOfWork.Category.Get(u => u.Id == product.CategoryId);
            }

            var productResponse = _mapper.Map<GetProductResponse>(product);
            return Task.FromResult(productResponse);
        }
    }
}
