using MediatR;
using Microsoft.EntityFrameworkCore;
using Pos.Application.Common.Interfaces;
using Pos.Application.Contracts.Response.Category;

namespace Pos.Application.Features.Category.Queries
{
    public class GetSubCategoryQuery : IRequest<GetSubCategoryResponse>
    {
        public string Name { get; }
        public GetSubCategoryQuery(string name)
        {
            Name = name;
        }

    }
    public class GetSubCategoryHandler : IRequestHandler<GetSubCategoryQuery, GetSubCategoryResponse>
    {
        private readonly IPosDbContext _dbContext;

        public GetSubCategoryHandler(IPosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetSubCategoryResponse> Handle(GetSubCategoryQuery request, CancellationToken cancellationToken)
        {
            var subCategories = await _dbContext.SubCategories.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            if (subCategories == null)
            {
                return null;
            }

            var response = new GetSubCategoryResponse
            {
                Name = subCategories.Name,
                Description = subCategories.Description
            };

            return response;
        }
    }
}