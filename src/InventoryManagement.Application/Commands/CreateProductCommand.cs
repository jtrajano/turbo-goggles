// Application/Products/Commands/CreateProduct/CreateProductCommand.cs
using FluentValidation;
using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Mapping;
using InventoryManagement.Domain.Entities;
using MediatR;

namespace InventoryManagement.Application.Products.Commands;

public record CreateProductCommand : IRequest<ProductDto>
{
    public string SKU { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Category { get; init; } = string.Empty;
    public string? Supplier { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal CostPrice { get; init; }
    public int CurrentStock { get; init; }
    public int ReorderLevel { get; init; } = 10;
    public int MaxStockLevel { get; init; } = 1000;
    public string? Unit { get; init; }
    public string? Barcode { get; init; }
    public string? ImageUrl { get; init; }
}

public class CreateProductCommandHandler(
    IProductRepository _productRepository
    ) : IRequestHandler<CreateProductCommand, ProductDto>
{
   


    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Check if SKU already exists
        if (await _productRepository.SkuExistsAsync(request.SKU, null, cancellationToken))
        {
            throw new InvalidOperationException($"Product with SKU '{request.SKU}' already exists.");
        }

        var product = Product.Create(
            request.SKU,
            request.Name,
            request.Category,
            request.UnitPrice,
            request.CostPrice,
            request.CurrentStock,
            request.ReorderLevel,
            request.MaxStockLevel,
            request.Description,
            request.Supplier,
            request.Unit,
            request.Barcode,
            request.ImageUrl
        );

        await _productRepository.AddAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return product.ToDto();
    }

    
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.SKU)
            .NotEmpty().WithMessage("SKU is required")
            .MaximumLength(50).WithMessage("SKU must not exceed 50 characters");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required")
            .MaximumLength(100).WithMessage("Category must not exceed 100 characters");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than 0");

        RuleFor(x => x.CostPrice)
            .GreaterThan(0).WithMessage("Cost price must be greater than 0");

        RuleFor(x => x.CurrentStock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");

        RuleFor(x => x.ReorderLevel)
            .GreaterThanOrEqualTo(0).WithMessage("Reorder level cannot be negative");

        RuleFor(x => x.MaxStockLevel)
            .GreaterThan(0).WithMessage("Max stock level must be greater than 0");

        RuleFor(x => x.Description)
            .MaximumLength(1000).When(x => !string.IsNullOrEmpty(x.Description));
    }
}