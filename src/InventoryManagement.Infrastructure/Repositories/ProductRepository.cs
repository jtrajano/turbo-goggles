using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        
    }
    public async Task<IEnumerable<Product>> SearchProductByText(string text)
    {
        text = text?.Trim().ToLower() ?? string.Empty;
        return await _db.AsNoTracking()
            .Where( p=>p.Name.ToLower().Contains(text) || p.Description.ToLower().Contains(text)).ToListAsync();
    }
}
