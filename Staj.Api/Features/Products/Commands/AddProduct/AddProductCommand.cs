using MediatR;
using StajWeb.Models.ViewModels;

namespace Staj.Api.Features.Products.Commands.AddProduct
{
    public class AddProductCommand : IRequest<ProductVM>
    {
        public ProductVM ProductVM { get; set; }
    }
}
