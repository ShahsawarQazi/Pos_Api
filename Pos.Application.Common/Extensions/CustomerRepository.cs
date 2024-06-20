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

        public async Task AddAsync(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
        }

        public void Update(Customer customer)
        {
            _context.Customer.Update(customer);
        }

        public async Task<Customer> FindByEmailAsync(string email)
        {
            return await _context.Customer.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customer.ToListAsync();
        }
    }
}