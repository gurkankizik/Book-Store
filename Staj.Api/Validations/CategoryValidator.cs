using FluentValidation;
using Staj.Dtos;

namespace Staj.Api.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .Length(3, 30).WithMessage("Category name must be between 3 and 30 characters.");
            RuleFor(x => x.DisplayOrder)
                .InclusiveBetween(1, 100).WithMessage("Category name must be between 1 and 100 characters.");
        }
    }
}
