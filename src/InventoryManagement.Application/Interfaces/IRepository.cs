using InventoryManagement.Domain.Entities;
using System.Linq.Expressions;

namespace InventoryManagement.Application.Interfaces;

public interface IRepository<T> where T : class, IBaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(T product, CancellationToken cancellationToken = default);
    Task UpdateAsync(T product, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<T> items, int totalCount)> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? filter, CancellationToken ct = default);  
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
