using AutoMapper;
using MediatR;
using StajWeb.DataAccess.Repository.IRepository;

namespace Staj.Api.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetProductsQueryHandler> _logger;
        private readonly IMapper _mapper;


        public GetProductsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetProductsQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<List<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _unitOfWork.Product.GetAll();
            var productResponses = _mapper.Map<List<GetProductsResponse>>(products);
            return Task.FromResult(productResponses);
        }
    }
}
