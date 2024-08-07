using MediatR;

namespace Staj.Api.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }
}
