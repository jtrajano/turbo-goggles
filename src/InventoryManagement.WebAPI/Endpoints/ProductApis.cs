using InventoryManagement.Application;
using InventoryManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.WebAPI.Endpoints;

public static class ProductApis
{
    public static void MapEndpoints(this WebApplication app)
    {
        // Map additional endpoints here
        var grp = app.MapGroup("/api/products");

        grp.MapGet("/{id}",async  (Guid id, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));

            return Results.Ok(result);
        });

        grp.MapGet("/",async ([FromQuery]  string text, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetProductSearchByTextQuery(text));
            return Results.Ok(result);
        });



    }





}
