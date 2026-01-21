using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Queries.Products;

public sealed record PageRequest(string filter ="", int pageNumber = 1, int pageSize = 10);

public sealed record PagedResult<T>(
    IReadOnlyList<T> Items,
    int TotalCount,
    int PageNumber,
    int PageSize
)
{
    public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);
}

public record GetPagedQuery(PageRequest? Page = null) : IRequest<PagedResult<ProductDto>>;

public class GetPagedQueryHandler(IApplicationDbContext _context) : IRequestHandler<GetPagedQuery, PagedResult<ProductDto>>
{

    public async Task<PagedResult<ProductDto>> Handle(GetPagedQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page ?? new PageRequest();

        var pageNumber = Math.Max(page.pageNumber, 1);
        var pageSize = Math.Max(page.pageSize, 1);
        var text = page.filter?.Trim() ?? string.Empty;

        var result = await GetPagedAsync(pageNumber, pageSize, text, cancellationToken );

        return new PagedResult<ProductDto>(
            result.items.ToList(), 
            result.totalCount, 
            pageNumber, 
            pageSize);
    }

    private async Task<(IReadOnlyList<ProductDto> items, int totalCount)> GetPagedAsync(
     int pageNumber,
     int pageSize,
     string text,
     CancellationToken ct = default)
    {
        var query = _context.Products.AsNoTracking()
            .Where(p => p.Name.ToLower().Contains(text));


        var result = await query
            .Select(p=>p.ToDto())
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        var totalCount = await query.CountAsync();

        return (result, totalCount);
    }
}