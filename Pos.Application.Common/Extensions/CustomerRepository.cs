using Microsoft.EntityFrameworkCore;
using Pos.Application.Common.Interfaces;
using Pos.Domain.Entities.Entities;

namespace Pos.Application.Common.Extensions
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IPosDbContext _context;

        public CustomerRepository(IPosDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
        }

        public Task Update(Customer customer)
        {
            _context.Customer.Update(customer);
            return Task.CompletedTask;
        }

        public async Task<Customer> FindByEmail(string email)
        {
            return (await _context.Customer.FirstOrDefaultAsync(c => c.Email == email))!;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customer.ToListAsync();
        }
    }
}