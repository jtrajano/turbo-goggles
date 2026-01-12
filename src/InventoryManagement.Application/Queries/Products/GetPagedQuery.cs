using InventoryManagement.Application.Mapping;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

public class GetPagedQueryHandler : IRequestHandler<GetPagedQuery, PagedResult<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetPagedQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<PagedResult<ProductDto>> Handle(GetPagedQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page ?? new PageRequest();

        var pageNumber = Math.Max(page.pageNumber, 1);
        var pageSize = Math.Max(page.pageSize, 1);
        var text = page.filter?.Trim() ?? string.Empty;

        var result = await _productRepository.GetPagedAsync(pageNumber, pageSize, 
            p => p.Name.ToLower().Contains(text), cancellationToken );

        return new PagedResult<ProductDto>(
            result.items.Select(p=> p.ToDto()).ToList(), 
            result.totalCount, 
            pageNumber, 
            pageSize);
    }
}