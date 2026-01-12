using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace InventoryManagement.Infrastructure.Repositories.Base;

public class BaseRepository<T>() : IRepository<T> where T : class, IBaseEntity
{
    private ApplicationDbContext _context;
    protected DbSet<T> _db;

    public BaseRepository(ApplicationDbContext context): this()
    {
        _context = context;
        _db = _context.Set<T>();
    }
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.FindAsync (id , cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _db.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _db.AddAsync(entity, cancellationToken);
      
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _db.Update(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            _db.Remove(entity);
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.AnyAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<int>  SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<T> items, int totalCount)> GetPagedAsync(
        int pageNumber, 
        int pageSize, 
        Expression<Func<T, bool>>? filter,
        CancellationToken ct = default)
    {
        var items = await _db.AsNoTracking()
            .Where(filter ?? (_ => true))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToListAsync(ct);
        var totalCount = await _db.CountAsync(ct);

        return (items, totalCount);
    }
}
