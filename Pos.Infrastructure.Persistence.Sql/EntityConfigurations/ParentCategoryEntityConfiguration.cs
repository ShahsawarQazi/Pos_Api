using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pos.Domain.Entities.Entities;

namespace Pos.Infrastructure.Persistence.Sql.EntityConfigurations
{
    public class ParentCategoryEntityConfiguration : IEntityTypeConfiguration<ParentCategory>
    {
        public void Configure(EntityTypeBuilder<ParentCategory> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
        }
    }
}
