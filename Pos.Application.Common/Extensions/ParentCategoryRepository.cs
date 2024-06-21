using Pos.Application.Common.Interfaces;
using Pos.Domain.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Pos.Application.Common.Models;

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

        public async Task UpdateAsync(UpdateParentCategoryRequest parentCategoryRequest)
        {
            var parentCategory = await _context.ParentCategories.FindAsync(parentCategoryRequest.Name);

            if (parentCategory != null)
            {
                parentCategory.Name = parentCategoryRequest.Name;
                parentCategory.Description = parentCategoryRequest.Description;

                _context.ParentCategories.Update(parentCategory);
                await _context.SaveChangesAsync();
            }
        }

    }
}
