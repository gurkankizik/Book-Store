﻿using FluentValidation;
using Staj.Api.Dtos;

namespace Staj.Api.Validations
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN is required");
            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required");
            RuleFor(x => x.ListPrice)
                .NotEmpty().WithMessage("List Price is required")
                .InclusiveBetween(1, 1000).WithMessage("List Price must be between 1 and 1000");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is required")
                .InclusiveBetween(1, 1000).WithMessage("Price must be between 1 and 1000");
            RuleFor(x => x.Price50)
                .NotEmpty().WithMessage("Price50 is required")
                .InclusiveBetween(1, 1000).WithMessage("Price50 must be between 1 and 1000");
            RuleFor(x => x.Price100)
                .NotEmpty().WithMessage("Price100 is required")
                .InclusiveBetween(1, 1000).WithMessage("Price100 must be between 1 and 1000");
            RuleFor(x => x.ImgUrl)
                .NotEmpty().WithMessage("Image is required");
        }
    }
}
