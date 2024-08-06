using MediatR;
using StajWeb.Models.ViewModels;

namespace Staj.Api.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductVM>
    {
        public ProductVM ProductVM { get; set; }
    }
}
