using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using MyRestaurantApi.Data;
namespace MyRestaurantApi;

public static class TogoOrderEndpoints
{
    public static void MapTogoOrderEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/TogoOrder").WithTags(nameof(TogoOrder));

        group.MapGet("/", async (MyRestaurantApiContext db) =>
        {
            return await db.TogoOrder.ToListAsync();
        })
        .WithName("GetAllTogoOrders")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<TogoOrder>, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            return await db.TogoOrder.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is TogoOrder model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetTogoOrderById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, TogoOrder togoOrder, MyRestaurantApiContext db) =>
        {
            var affected = await db.TogoOrder
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, togoOrder.Id)
                    .SetProperty(m => m.OrderCreated, togoOrder.OrderCreated)
                    .SetProperty(m => m.Subtotal, togoOrder.Subtotal)
                    .SetProperty(m => m.Tax, togoOrder.Tax)
                    .SetProperty(m => m.Total, togoOrder.Total)
                    .SetProperty(m => m.PaymentMethod, togoOrder.PaymentMethod)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateTogoOrder")
        .WithOpenApi();

        group.MapPost("/", async (TogoOrder togoOrder, MyRestaurantApiContext db) =>
        {
            db.TogoOrder.Add(togoOrder);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/TogoOrder/{togoOrder.Id}",togoOrder);
        })
        .WithName("CreateTogoOrder")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            var affected = await db.TogoOrder
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteTogoOrder")
        .WithOpenApi();
    }
}
