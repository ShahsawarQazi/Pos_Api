using MediatR;
using Microsoft.EntityFrameworkCore;
using Pos.Application.Common.Interfaces;
using Pos.Application.Contracts.Response.Category;

namespace Pos.Application.Features.Category.Queries
{
    public class GetParentCategoryQuery : IRequest<GetParentCategoryResponse>
    {
        public string Name { get; }
        public GetParentCategoryQuery(string name)
        {
            Name = name;
        }
       
    }
    public class GetParentCategoryHandler : IRequestHandler<GetParentCategoryQuery, GetParentCategoryResponse>
    {
        private readonly IPosDbContext _dbContext;

        public GetParentCategoryHandler(IPosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetParentCategoryResponse> Handle(GetParentCategoryQuery request, CancellationToken cancellationToken)
        {
            var parentCategories = await _dbContext.ParentCategories.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            if (parentCategories == null)
            {
                return null;
            }

            var response = new GetParentCategoryResponse
            {
                Name = parentCategories.Name,
                Description = parentCategories.Description
            };

            return response;
        }
    }
}
