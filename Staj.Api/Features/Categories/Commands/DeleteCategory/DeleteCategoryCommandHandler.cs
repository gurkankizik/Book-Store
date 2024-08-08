using AutoMapper;
using MediatR;
using StajWeb.DataAccess.Repository.IRepository;

namespace Staj.Api.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteCategoryCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteCategoryCommandHandler> logger, IMapper mapper)
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
                _logger.LogWarning($"Category with ID {request.Id} not found.");
                throw new KeyNotFoundException($"Category with ID {request.Id} not found.");
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            return Task.CompletedTask;
        }
    }
}
