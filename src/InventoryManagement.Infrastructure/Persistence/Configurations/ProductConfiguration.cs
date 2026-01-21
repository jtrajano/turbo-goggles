// Infrastructure/Persistence/Configurations/ProductConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();  // Since you're using Guid.NewGuid() in entity

        builder.Property(p => p.SKU)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Category)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Supplier)
            .HasMaxLength(100);

        builder.Property(p => p.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.CostPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.CurrentStock)
            .IsRequired();

        builder.Property(p => p.ReorderLevel)
            .IsRequired();

        builder.Property(p => p.MaxStockLevel)
            .IsRequired();

        builder.Property(p => p.Unit)
            .HasMaxLength(50);

        builder.Property(p => p.ImageUrl)
            .HasMaxLength(500);

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(p => p.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(p => p.CreatedBy)
            .HasMaxLength(100);

        builder.Property(p => p.UpdatedBy)
            .HasMaxLength(100);

        // Indexes
        builder.HasIndex(p => p.SKU).IsUnique();
        builder.HasIndex(p => p.Category);
        builder.HasIndex(p => p.IsActive);
    }
}

