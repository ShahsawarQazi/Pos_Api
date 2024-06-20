using MediatR;
using Pos.Application.Contracts.Request.Category;
using Pos.Application.Contracts.Response.Category;

namespace Pos.Application.Features.Category.Command
{
    public class CreateParentCategoryCommand : IRequest<bool>
    {
        public CreateParentCategoryRequest Request { get; }
        public CreateParentCategoryCommand(CreateParentCategoryRequest request)
        {
            Request = request;
        }
    }

    public class CreateParentCategoryHandler : IRequestHandler<CreateParentCategoryCommand, bool>
    {
        public Task<bool> Handle(CreateParentCategoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
