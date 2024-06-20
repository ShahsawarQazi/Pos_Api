using Pos.Application.Common.Interfaces;
using Pos.Domain.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Pos.Application.Common.Extensions
{
    public class ParentCategoryRepository : IParentCategoryRepository
    {
        private readonly IPosDbContext _context;

        public ParentCategoryRepository(IPosDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ParentCategory parentCategory)
        {
            await _context.ParentCategories.AddAsync(parentCategory);
        }

        public async Task<IEnumerable<ParentCategory>> GetAllAsync()
        {
            return await _context.ParentCategories.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
