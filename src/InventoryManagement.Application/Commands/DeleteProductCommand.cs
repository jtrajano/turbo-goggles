// Application/Products/Commands/DeleteProduct/DeleteProductCommand.cs
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Exceptions;
using MediatR;

namespace InventoryManagement.Application.Products.Commands;

public record DeleteProductCommand(Guid Id) : IRequest<Unit>;

public class DeleteProductCommandHandler(IProductRepository _productRepository) : IRequestHandler<DeleteProductCommand, Unit>
{
    
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }

        _productRepository.Delete(product);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}


