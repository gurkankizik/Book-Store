using AutoMapper;
using MediatR;
using Staj.Api.Dtos;
using StajWeb.DataAccess.Repository.IRepository;

namespace Staj.Api.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteCategoryCommand> _logger;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteCategoryCommand> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _unitOfWork.Category.Get(u => u.Id == request.Id);
            if (category == null)
            {
                _logger.LogWarning($"Product with ID {request.Id} not found.");
                return null;
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Task.CompletedTask;
        }
    }
}
