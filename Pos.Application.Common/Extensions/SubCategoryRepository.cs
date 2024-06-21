using Pos.Application.Common.Interfaces;
using Pos.Domain.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Pos.Application.Common.Models;

namespace Pos.Application.Common.Extensions
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly IPosDbContext _context;

        public SubCategoryRepository(IPosDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SubCategory SubCategory)
        {
            await _context.SubCategories.AddAsync(SubCategory);
        }

        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            return await _context.SubCategories.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateSubCategoryRequest SubCategoryRequest)
        {
            var SubCategory = await _context.SubCategories.FindAsync(SubCategoryRequest.Name);

            if (SubCategory != null)
            {
                SubCategory.Name = SubCategoryRequest.Name;
                SubCategory.Description = SubCategoryRequest.Description;

                _context.SubCategories.Update(SubCategory);
                await _context.SaveChangesAsync();
            }
        }

    }
}
