using MediatR;

namespace Staj.Api.Features.Products.Queries.GetProducts
{
    public class GetProductsQuery() : IRequest<List<GetProductsResponse>>
    {
    }
}
