using AutoMapper;
using MediatR;
using StajWeb.DataAccess.Repository.IRepository;

namespace Staj.Api.Features.Categories.Queries.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetCategoryQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCategoryQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public Task<GetCategoryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = _unitOfWork.Category.Get(u => u.Id == request.Id);
            if (category == null)
            {
                _logger.LogWarning($"Category with ID {request.Id} not found.");
                return Task.FromResult<GetCategoryResponse>(null);
            }

            var categoryResponse = _mapper.Map<GetCategoryResponse>(category);
            return Task.FromResult(categoryResponse);
        }
    }
}
