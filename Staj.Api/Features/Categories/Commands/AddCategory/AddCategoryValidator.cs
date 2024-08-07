using FluentValidation;
using Staj.Api.Features.Categories.Commands.AddCategory;

namespace Staj.Api.Features.Categories.Commands.AddCategoryValidator
{
    public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .Length(3, 30).WithMessage("Category name must be between 3 and 30 characters.");
            RuleFor(x => x.DisplayOrder)
                .InclusiveBetween(1, 100).WithMessage("Category name must be between 1 and 100 characters.");
        }
    }
}
