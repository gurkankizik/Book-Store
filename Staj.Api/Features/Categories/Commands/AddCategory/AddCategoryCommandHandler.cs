using AutoMapper;
using FluentValidation;
using MediatR;
using StajWeb.DataAccess.Repository.IRepository;
using StajWeb.Models;

namespace Staj.Api.Features.Categories.Commands.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, AddCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddCategoryCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<AddCategoryCommand> _validator;

        public AddCategoryCommandHandler(IUnitOfWork unitOfWork, ILogger<AddCategoryCommandHandler> logger, IMapper mapper, IValidator<AddCategoryCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        public Task<AddCategoryResponse> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);

            if (result.IsValid)
            {
                var category = _mapper.Map<Category>(request);
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                var response = _mapper.Map<AddCategoryResponse>(category);
                return Task.FromResult(response);
            }
            throw new ValidationException(result.Errors);
        }
    }
}
