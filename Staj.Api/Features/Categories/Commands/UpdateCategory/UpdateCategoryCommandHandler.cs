using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<UpdateCategoryCommand> _validator;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateCategoryCommandHandler> logger, IMapper mapper, IValidator<UpdateCategoryCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        public Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            //var existingCategory = _unitOfWork.Category.Get(u => u.Id == request.Id);
            //if (existingCategory == null)
            //{
            //    _logger.LogWarning($"Category with ID {request.Id} not found.");
            //    throw new KeyNotFoundException($"Category with ID {request.Id} not found.");
            //}

            var result = _validator.Validate(request);
            if (result.IsValid)
            {
                var category = _mapper.Map<Category>(request);
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                var response = _mapper.Map<UpdateCategoryResponse>(category);
                return Task.FromResult(response);
            }
            throw new ValidationException(result.Errors);
        }
    }
}
