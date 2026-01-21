using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Mapping;
using MediatR;

namespace InventoryManagement.Application.Queries.Products;

public record SearchByTextQuery(string text) : IRequest<ICollection<ProductDto>>;

public class SearchByTextQueryHandler(IProductRepository _repository) : IRequestHandler<SearchByTextQuery, ICollection<ProductDto>>
{
    public async Task<ICollection<ProductDto>> Handle(SearchByTextQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.SearchByText(request.text);

        return products.Select(p => p.ToDto()).ToList();

    }
}