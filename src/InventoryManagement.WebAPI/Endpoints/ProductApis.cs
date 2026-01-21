using InventoryManagement.Application.Products.Commands;
using InventoryManagement.Application.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.WebAPI.Endpoints;

public static class ProductApis
{

    public static void MapEndpoint(this WebApplication app)
    {
        app.MapProductEndpoint();
        app.MapUserEndpoint();
    }
    public static void MapProductEndpoint(this WebApplication app)
    {
        // Map additional endpoints here
        var grp = app.MapGroup("/api/products");

        grp.MapGet("/{id}",async  (Guid id, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetByIdQuery(id));

            return Results.Ok(result);
        });

        grp.MapGet("/",async ([FromQuery]  string text, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new SearchByTextQuery(text));
            return Results.Ok(result);
        });
        grp.MapPost("/", async ([FromBody] CreateProductCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });
        grp.MapPut("/", async ([FromBody] UpdateProductCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });

        grp.MapDelete("/", async ([FromBody] DeleteProductCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result);
        });

        grp.MapGet("/paging", async (
            [AsParameters] PageRequest param,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetPagedQuery(param));
            return Results.Ok(result);
        });

    }
}
