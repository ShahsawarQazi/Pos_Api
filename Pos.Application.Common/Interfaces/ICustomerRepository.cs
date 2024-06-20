using Pos.Domain.Entities.Entities;

namespace Pos.Application.Common.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        void Update(Customer customer);
        Task<Customer> FindByEmailAsync(string email);
        Task SaveChangesAsync();
        Task<IEnumerable<Customer>> GetAllAsync();
    }
}
