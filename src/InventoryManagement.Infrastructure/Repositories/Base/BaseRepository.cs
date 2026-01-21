using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace InventoryManagement.Infrastructure.Repositories.Base;

public class BaseRepository<T>() : IRepository<T> where T : class, IBaseEntity
{
    public IApplicationDbContext _context;
    protected DbSet<T> _db;

    public BaseRepository(IApplicationDbContext context): this()
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

    public void Update(T entity)
    {
        _db.Update(entity);
    }

    public void Delete(T entity)
    {
       _db.Remove(entity);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.AnyAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<int>  SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

 
}
