using AutoMapper;
using MediatR;
using Staj.Api.Features.Products.Queries.GetProducts;
using StajWeb.DataAccess.Repository.IRepository;
using StajWeb.Models;

namespace Staj.Api.Features.Categories.Commands.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, AddCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetProductsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public AddCategoryCommandHandler(IUnitOfWork unitOfWork, ILogger<GetProductsQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<AddCategoryResponse> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            var response = _mapper.Map<AddCategoryResponse>(category);
            return Task.FromResult(response);
        }
    }
}
