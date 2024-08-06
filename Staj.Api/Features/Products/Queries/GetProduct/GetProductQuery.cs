using MediatR;

namespace Staj.Api.Features.Products.Queries.GetProduct
{
    public class GetProductQuery() : IRequest<GetProductResponse>
    {
        public int Id { get; set; }
    }
}
