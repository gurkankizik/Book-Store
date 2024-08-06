using MediatR;

namespace Staj.Api.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<DeleteCategoryResponse>
    {
        public int Id { get; set; }
    }
}
