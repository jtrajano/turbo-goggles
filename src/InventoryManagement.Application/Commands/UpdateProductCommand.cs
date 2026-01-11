using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using MediatR;

namespace InventoryManagement.Application;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Stock
) : IRequest<Result>;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IProductRepository _repository;

    public UpdateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
            return Result.Failure("Product not found");

        try
        {
            product.Update(request.Name, request.Description, request.Price, request.Stock);
            await _repository.UpdateAsync(product, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (ArgumentException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}