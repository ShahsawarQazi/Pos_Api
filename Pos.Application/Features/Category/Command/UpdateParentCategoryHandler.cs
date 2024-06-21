using MediatR;
using Pos.Application.Common.Interfaces;
using Pos.Application.Contracts.Request.Category;

namespace Pos.Application.Features.Category.Command
{
    public class UpdateParentCategoryCommand : IRequest<bool>
    {
        public UpdateParentCategoryCommand(UpdateParentCategoryRequest request)
        {
            Request = request;
        }

        public UpdateParentCategoryRequest Request { get; }
    }
    public class UpdateParentCategoryHandler : IRequestHandler<UpdateParentCategoryCommand, bool>
    {
        private readonly IParentCategoryRepository _parentCategoryRepository;

        public UpdateParentCategoryHandler(IParentCategoryRepository parentCategoryRepository)
        {
            _parentCategoryRepository = parentCategoryRepository;
        }

        public async Task<bool> Handle(UpdateParentCategoryCommand request, CancellationToken cancellationToken)
        {
            var updateRequest = new Common.Models.UpdateParentCategoryRequest
            {
                Name = request.Request.Name,
                Description = request.Request.Description
            };

            await _parentCategoryRepository.UpdateAsync(updateRequest);
            return true;
        }
    }
}
