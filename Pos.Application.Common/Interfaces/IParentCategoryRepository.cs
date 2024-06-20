using Pos.Domain.Entities.Entities;

namespace Pos.Application.Common.Interfaces
{
    public interface IParentCategoryRepository
    {
        Task AddAsync(ParentCategory parentCategory);
        Task<IEnumerable<ParentCategory>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
