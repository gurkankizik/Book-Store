using AutoMapper;
using MediatR;
using Staj.Api.Features.Products.Queries.GetProducts;
using StajWeb.DataAccess.Repository.IRepository;
using StajWeb.Models;
using StajWeb.Models.ViewModels;

namespace Staj.Api.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductVM>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetProductsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, ILogger<GetProductsQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<ProductVM> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.ProductVM.Product);
            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();
            var response = _mapper.Map<ProductVM>(product);
            return Task.FromResult(response);
        }
    }
}
