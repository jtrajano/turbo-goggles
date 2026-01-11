using InventoryManagement.Application;
using InventoryManagement.Application.Queries;
using InventoryManagement.Application.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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



    }





}
