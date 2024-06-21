using Microsoft.EntityFrameworkCore;
using Pos.Application.Common.Interfaces;
using Pos.Domain.Entities.Entities;

namespace Pos.Application.Common.Extensions
{
    public class MenuRepository : IMenuRepository
    {
        private readonly IPosDbContext _context;

        public MenuRepository(IPosDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddMenu(Menu menu)
        {
            await _context.Menu.AddAsync(menu);
        }

        public Task UpdateMenu(Menu menu)
        {
            _context.Menu.Update(menu);
            return Task.CompletedTask;
        }

        public async Task<Menu> FindByMenu(string name)
        {
            return (await _context.Menu.FirstOrDefaultAsync(c => c.Name == name))!;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}