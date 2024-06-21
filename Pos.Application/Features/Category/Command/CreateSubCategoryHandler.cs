using MediatR;
using Pos.Application.Common.Interfaces;
using Pos.Application.Contracts.Request.Category;
using Pos.Domain.Entities.Entities;

namespace Pos.Application.Features.Category.Command
{
    public class CreateSubCategoryCommand : IRequest<bool>
    {
        public CreateSubCategoryRequest Request { get; }
        public CreateSubCategoryCommand(CreateSubCategoryRequest request)
        {
            Request = request;
        }
    }

    public class CreateSubCategoryHandler : IRequestHandler<CreateSubCategoryCommand, bool>
    {
        private readonly ISubCategoryRepository _SubCategoryRepository;

        public CreateSubCategoryHandler(ISubCategoryRepository SubCategoryRepository)
        {
            _SubCategoryRepository = SubCategoryRepository;
        }

        public async Task<bool> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var SubCategory = new SubCategory
            {
                Name = request.Request.Name,
                Description = request.Request.Description 
            };
            await _SubCategoryRepository.AddAsync(SubCategory);
            await _SubCategoryRepository.SaveChangesAsync();

            return true;
        }
    }
}
