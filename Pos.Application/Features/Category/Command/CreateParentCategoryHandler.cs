using MediatR;
using Pos.Application.Common.Interfaces;
using Pos.Application.Contracts.Request.Category;
using Pos.Domain.Entities.Entities;

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
        private readonly IParentCategoryRepository _parentCategoryRepository;

        public CreateParentCategoryHandler(IParentCategoryRepository parentCategoryRepository)
        {
            _parentCategoryRepository = parentCategoryRepository;
        }

        public async Task<bool> Handle(CreateParentCategoryCommand request, CancellationToken cancellationToken)
        {
            var parentCategory = new ParentCategory
            {
                Name = request.Request.Name,
                Description = request.Request.Description 
            };
            await _parentCategoryRepository.AddAsync(parentCategory);
            await _parentCategoryRepository.SaveChangesAsync();

            return true;
        }
    }
}
