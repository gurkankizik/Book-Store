using MediatR;

namespace Staj.Api.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<List<GetCategoriesResponse>>
    {
    }
}
