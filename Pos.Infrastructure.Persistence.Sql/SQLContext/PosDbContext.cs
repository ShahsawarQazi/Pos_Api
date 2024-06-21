using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Pos.Application.Common.Interfaces;
using Pos.Domain.Entities.Entities;
using Pos.Domain.Entities.Enums;

namespace Pos.Infrastructure.Persistence.Sql.SQLContext
{
    public sealed class PosDbContext : DbContext, IPosDbContext
    {
        public PosDbContext()
        { }
        public PosDbContext(DbContextOptions<PosDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customer { get; set; } = null!;
        public DbSet<ParentCategory> ParentCategories { get; set; } = null!;
        public DbSet<Menu> Menu { get; set; } = null!;
        public DbSet<SubCategory> SubCategories { get; set; } = null!;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new(),
            OperationType operationType = OperationType.Default, Guid transactionId = default, bool fullMode = true, bool forMigration = false)
        {

            var result = await SaveChangesWithClear(cancellationToken, forMigration);


            return result;
        }
        private async Task<int> SaveChangesWithClear(CancellationToken cancellationToken, bool forMigration)
        {
            int result;
            try
            {
                result = await base.SaveChangesAsync(cancellationToken);
            }
            finally
            {
                if (!forMigration)
                {
                    Clear();
                }
            }

            return result;
        }
        public void Clear()
        {
            ChangeTracker.Clear();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            if (!options.IsConfigured)
            {
                // will be hit by migration deployments. By migrations deployments default constructor is used, and then have to configure Sql server.
                options.UseSqlServer();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


      

    }
}
