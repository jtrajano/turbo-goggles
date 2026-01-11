using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Mapping;
using MediatR;

namespace InventoryManagement.Application.Queries;

public record GetProductSearchByTextQuery(string text) : IRequest<ICollection<ProductDto>>;

public class GetProductSearchByTextQueryHandler(IProductRepository _repository) : IRequestHandler<GetProductSearchByTextQuery, ICollection<ProductDto>>
{
    public async Task<ICollection<ProductDto>> Handle(GetProductSearchByTextQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.SearchProductByText(request.text);

        return products.Select(p => p.ToDto()).ToList();

    }
}