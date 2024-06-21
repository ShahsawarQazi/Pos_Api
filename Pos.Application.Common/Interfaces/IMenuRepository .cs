using Pos.Domain.Entities.Entities;

namespace Pos.Application.Common.Interfaces
{
    public interface IMenuRepository
    {
        Task AddMenu(Menu menu);
        Task UpdateMenu(Menu menu);
        Task<Menu> FindByMenu(string name);
        Task SaveChanges();
    }
}
