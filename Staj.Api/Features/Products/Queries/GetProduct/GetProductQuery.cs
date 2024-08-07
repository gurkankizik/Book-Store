using MediatR;
using StajWeb.Models;

namespace Staj.Api.Features.Products.Queries.GetProduct
{
    public class GetProductQuery() : IRequest<GetProductResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public double ListPrice { get; set; }
        public double Price { get; set; }
        public double Price50 { get; set; }
        public double Price100 { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string ImgUrl { get; set; }
    }
}
