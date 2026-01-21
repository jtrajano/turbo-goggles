using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Mapping;
using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Exceptions;
using MediatR;

namespace InventoryManagement.Application;

public record UpdateProductCommand : IRequest<ProductDto>
{
    public Guid Id { get; init; }
    public string SKU { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Category { get; init; } = string.Empty;
    public string? Supplier { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal CostPrice { get; init; }
    public int ReorderLevel { get; init; }
    public int MaxStockLevel { get; init; }
    public string? Unit { get; init; }
    public string? Barcode { get; init; }
    public string? ImageUrl { get; init; }
    public bool IsActive { get; init; }
}



public class UpdateProductCommandHandler(IProductRepository _productRepository) : IRequestHandler<UpdateProductCommand, ProductDto>
{

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }

        // Check if SKU already exists for another product
        if (await _productRepository.SkuExistsAsync(request.SKU, request.Id, cancellationToken))
        {
            throw new InvalidOperationException($"Product with SKU '{request.SKU}' already exists.");
        }

        product.Update(
            request.SKU,
            request.Name,
            request.Category,
            request.UnitPrice,
            request.CostPrice,
            request.ReorderLevel,
            request.MaxStockLevel,
            request.Description,
            request.Supplier,
            request.Unit,
            request.Barcode,
            request.ImageUrl
        );

        if (request.IsActive)
            product.Activate();
        else
            product.Deactivate();

        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return product.ToDto();
    }
}
