using AutoMapper;
using MediatR;
using StajWeb.DataAccess.Repository.IRepository;

namespace Staj.Api.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<GetCategoriesResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetCategoriesQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCategoriesQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<List<GetCategoriesResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var category = _unitOfWork.Category.GetAll();
            var categoryResponse = _mapper.Map<List<GetCategoriesResponse>>(category);
            return Task.FromResult(categoryResponse);
        }
    }
}
