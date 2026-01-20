using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.HasIndex(e => e.SKU).IsUnique();

        // Create index on Category for filtering
        entity.HasIndex(e => e.Category);

        // Set default values
        entity.Property(e => e.IsActive).HasDefaultValue(true);
        entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
    }
}
