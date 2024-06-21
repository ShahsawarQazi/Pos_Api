using Pos.Domain.Entities.Entities;

namespace Pos.Application.Common.Interfaces
{
    public interface ICustomerRepository
    {
        Task Add(Customer customer);
        Task Update(Customer customer);
        Task<Customer> FindByEmail(string email);
        Task SaveChanges();
        Task<IEnumerable<Customer>> GetAll();
    }
}
