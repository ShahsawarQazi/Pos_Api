using Pos.Domain.Entities.Entities;

namespace Pos.Application.Common.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task SaveChangesAsync();
    }
}
