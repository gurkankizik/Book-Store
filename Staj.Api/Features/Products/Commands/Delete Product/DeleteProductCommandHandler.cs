﻿using AutoMapper;
using MediatR;
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
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {request.Id} not found.");
                throw new KeyNotFoundException($"Product with ID {request.Id} not found.");
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            return Task.CompletedTask;
        }
    }
}