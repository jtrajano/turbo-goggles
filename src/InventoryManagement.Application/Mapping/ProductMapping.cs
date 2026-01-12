using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Mapping;

public static class ProductMapping
{
    public static ProductDto ToDto( this Product product)
    {
        return new ProductDto(
             product.Id,
             product.Name,
             product.Description,
             product.Price,
             product.Stock,
             product.ImageUrl ?? string.Empty,
             product.CreatedAt,
             product.UpdatedAt
         );
    }
}
