using MediatR;
using Pos.Application.Common.Interfaces;
using Pos.Application.Contracts.Request.Category;

namespace Pos.Application.Features.Category.Command
{
    public class UpdateSubCategoryCommand : IRequest<bool>
    {
        public UpdateSubCategoryCommand(UpdateSubCategoryRequest request)
        {
            Request = request;
        }

        public UpdateSubCategoryRequest Request { get; }
    }
    public class UpdateSubCategoryHandler : IRequestHandler<UpdateSubCategoryCommand, bool>
    {
        private readonly ISubCategoryRepository _SubCategoryRepository;

        public UpdateSubCategoryHandler(ISubCategoryRepository SubCategoryRepository)
        {
            _SubCategoryRepository = SubCategoryRepository;
        }

        public async Task<bool> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var updateRequest = new Common.Models.UpdateSubCategoryRequest
            {
                Name = request.Request.Name,
                Description = request.Request.Description
            };

            await _SubCategoryRepository.UpdateAsync(updateRequest);
            return true;
        }
    }
}
