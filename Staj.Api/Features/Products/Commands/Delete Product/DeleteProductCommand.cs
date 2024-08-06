using MediatR;

namespace Staj.Api.Features.Products.Commands.Delete_Product
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }
    }
}
