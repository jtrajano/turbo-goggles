using InventoryManagement.Application.DTOs;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Mapping;

public static class ProductMapping
{
    public static ProductDto ToDto( this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            SKU = product.SKU,
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Supplier = product.Supplier,
            UnitPrice = product.UnitPrice,
            CostPrice = product.CostPrice,
            CurrentStock = product.CurrentStock,
            ReorderLevel = product.ReorderLevel,
            MaxStockLevel = product.MaxStockLevel,
            Unit = product.Unit,
            ImageUrl = product.ImageUrl,
            IsActive = product.IsActive,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            CreatedBy = product.CreatedBy,
            UpdatedBy = product.UpdatedBy,
            IsLowStock = product.IsLowStock(),
            StockValue = product.CalculateStockValue()
        };
    }
}
