using InventoryManagement.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.WebAPI;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        // Map additional endpoints here
        var grp = app.MapGroup("/api/products");

        grp.MapGet("/{id}", (Guid id, [FromServices] ISender sender) =>
        {
            sender.Send(new GetProductByIdQuery(id));
        });
        

        
    }
   
   



}
