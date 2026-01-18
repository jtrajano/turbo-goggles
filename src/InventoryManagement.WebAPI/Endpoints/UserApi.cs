
using InventoryManagement.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.WebAPI.Endpoints;

public static class UserApi
{
    public static void MapUserEndpoint(this WebApplication app)
    {
        // Map additional endpoints here
        var grp = app.MapGroup("/api/users");

        grp.MapGet("/", async ([FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetAllQuery());

            return Results.Ok(result);
        });

        grp.MapGet("/{id}", async (Guid id, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetByIdQuery(id));

            return Results.Ok(result);
        });

        grp.MapGet("/search", async ([FromQuery] string text, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new SearchByTextQuery(text));
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
