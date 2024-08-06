using AutoMapper;
using MediatR;
using StajWeb.DataAccess.Repository.IRepository;
using StajWeb.Models;

namespace Staj.Api.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateCategoryCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
            var response = _mapper.Map<UpdateCategoryResponse>(category);
            return Task.FromResult(response);
        }
    }
}
