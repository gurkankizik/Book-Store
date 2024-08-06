using MediatR;

namespace Staj.Api.Features.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<GetCategoryResponse>
    {
        public int Id { get; set; }
    }
}
