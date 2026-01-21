using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Mapping;
using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using MediatR;

namespace InventoryManagement.Application.Queries.Products;

public record GetAllQuery : IRequest<IEnumerable<ProductDto>>;

public class GetAllQueryHandler: IRequestHandler<GetAllQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    public GetAllQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<IEnumerable<ProductDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        return products.Select(product => product.ToDto()).ToList().AsReadOnly();
    }
}
