using InventoryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
             product.CreatedAt,
             product.UpdatedAt
         );
    }
}
