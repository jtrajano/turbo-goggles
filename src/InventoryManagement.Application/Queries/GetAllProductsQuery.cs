using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using MediatR;

namespace InventoryManagement.Application;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;

public class GetAllProductsQueryHandler: IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IRepository<Product> _productRepository;
    public GetAllProductsQueryHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        return products.Select(product => new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock,
            product.CreatedAt,
            product.UpdatedAt
        ));
    }
}
