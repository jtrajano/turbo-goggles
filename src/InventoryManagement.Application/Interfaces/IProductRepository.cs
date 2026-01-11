using InventoryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> SearchProductByText(string text);
}
