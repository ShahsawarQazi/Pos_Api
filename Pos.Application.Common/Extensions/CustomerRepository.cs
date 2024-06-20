using Microsoft.EntityFrameworkCore;
using Pos.Domain.Entities.Entities;
using Pos.Application.Common.Interfaces;

namespace Pos.Infrastructure.Repositories
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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}