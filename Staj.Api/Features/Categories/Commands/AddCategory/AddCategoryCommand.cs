using MediatR;

namespace Staj.Api.Features.Categories.Commands.AddCategory
{
    public class AddCategoryCommand : IRequest<AddCategoryResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
