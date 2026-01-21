using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Mapping;
using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Queries.Users;

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

public record GetPagedQuery(PageRequest? Page = null) : IRequest<PagedResult<UserDto>>;

public class GetPagedQueryHandler(IApplicationDbContext _context) 
    : IRequestHandler<GetPagedQuery, PagedResult<UserDto>>
{
    public async Task<PagedResult<UserDto>> Handle(GetPagedQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page ?? new PageRequest();

        var pageNumber = Math.Max(page.pageNumber, 1);
        var pageSize = Math.Max(page.pageSize, 1);
        var text = page.filter?.Trim() ?? string.Empty;

        var result = await GetPagedAsync(pageNumber, pageSize, text, cancellationToken );

        return new PagedResult<UserDto>(
            result.items.ToList(), 
            result.totalCount, 
            pageNumber, 
            pageSize);
    }

    private async Task<(IReadOnlyList<UserDto> items, int totalCount)> GetPagedAsync(
    int pageNumber,
    int pageSize,
    string text,
    CancellationToken ct = default)
    {
        var query = _context.Users.AsNoTracking()
            .Where(p => p.Email.ToLower().Contains(text) ||
            p.FirstName.ToLower().Contains(text) ||
            p.LastName.ToLower().Contains(text));


        var result = await query
            .Select(p => p.ToDto())
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize) 
            .ToListAsync(ct);

        var totalCount = await query.CountAsync();

        return (result, totalCount);
    }
}