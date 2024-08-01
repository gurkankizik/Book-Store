using FluentValidation;

namespace Staj.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
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
