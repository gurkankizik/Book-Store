using AutoMapper;
using MediatR;
using Staj.Api.Dtos;
using Staj.Api.Features.Products.Queries.GetProducts;
using StajWeb.DataAccess.Repository.IRepository;

namespace Staj.Api.Features.Products.Commands.Delete_Product
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetProductsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, ILogger<GetProductsQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = _unitOfWork.Product.Get(u => u.Id == request.Id);
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            var productDto = _mapper.Map<ProductDto>(product);
            return Task.CompletedTask;
        }
    }
}
