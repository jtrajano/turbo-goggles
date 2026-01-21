using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(IApplicationDbContext _context) : base(_context)
    {
        
    }

 
    public async Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.SKU == sku, cancellationToken);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> SkuExistsAsync(string sku, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.Where(p => p.SKU == sku);

        if (excludeId.HasValue)
        {
            query = query.Where(p => p.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }
    public async Task<IEnumerable<Product>> SearchByText(string text)
    {
        text = text?.Trim().ToLower() ?? string.Empty;
        return await _db.AsNoTracking()
            .Where( p=>p.Name.ToLower().Contains(text) || p.Description.ToLower().Contains(text)).ToListAsync();
    }
}
