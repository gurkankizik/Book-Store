using FluentValidation;
using StajWeb.Models.ViewModels;

namespace Staj.Api.Validations
{
    public class ProductValidator : AbstractValidator<ProductVM>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Product.Title)
                .NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Product.ISBN)
                .NotEmpty().WithMessage("ISBN is required");
            RuleFor(x => x.Product.Author)
                .NotEmpty().WithMessage("Author is required");
            RuleFor(x => x.Product.ListPrice)
                .NotEmpty().WithMessage("List Price is required")
                .InclusiveBetween(1, 1000).WithMessage("List Price must be between 1 and 1000");
            RuleFor(x => x.Product.Price)
                .NotEmpty().WithMessage("Price is required")
                .InclusiveBetween(1, 1000).WithMessage("Price must be between 1 and 1000");
            RuleFor(x => x.Product.Price50)
                .NotEmpty().WithMessage("Price50 is required")
                .InclusiveBetween(1, 1000).WithMessage("Price50 must be between 1 and 1000");
            RuleFor(x => x.Product.Price100)
                .NotEmpty().WithMessage("Price100 is required")
                .InclusiveBetween(1, 1000).WithMessage("Price100 must be between 1 and 1000");
            RuleFor(x => x.Product.ImgUrl)
                .NotEmpty().WithMessage("Image is required");
        }
    }
}
