using Pos.Application.Common.Models;
using Pos.Domain.Entities.Entities;

namespace Pos.Application.Common.Interfaces
{
    public interface ISubCategoryRepository
    {
        Task AddAsync(SubCategory subCategory);
        Task<IEnumerable<SubCategory>> GetAllAsync();
        Task SaveChangesAsync();
        Task UpdateAsync(UpdateSubCategoryRequest subCategoryRequest);
    }
}
