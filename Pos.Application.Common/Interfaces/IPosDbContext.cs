using Microsoft.EntityFrameworkCore;
using Pos.Domain.Entities.Entities;
using Pos.Domain.Entities.Enums;

namespace Pos.Application.Common.Interfaces
{
    public interface IPosDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken(),
                   OperationType operationType = OperationType.Default, Guid transactionId = default(Guid), bool fullMode = true, bool forMigration = false);
        DbSet<Customer> Customer { get; }
        DbSet<Menu> Menu { get; }
        DbSet<ParentCategory> ParentCategories { get; }
        DbSet<SubCategory> SubCategories { get; }
    }
}
