using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Mapping;
using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using MediatR;

namespace InventoryManagement.Application.Queries.Products;

public record GetByIdQuery(Guid Id) : IRequest<ProductDto?>;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ProductDto?>
{
    private readonly IProductRepository _productRepository;
    public GetByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ProductDto?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            return null;
        return product.ToDto();
    }
}
