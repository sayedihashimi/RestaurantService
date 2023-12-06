using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using MyRestaurantApi.Data;
namespace MyRestaurantApi;

public static class MenuItemOrderedEndpoints
{
    public static void MapMenuItemOrderedEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/MenuItemOrdered").WithTags(nameof(MenuItemOrdered));

        group.MapGet("/", async (MyRestaurantApiContext db) =>
        {
            return await db.MenuItemOrdered.ToListAsync();
        })
        .WithName("GetAllMenuItemOrdereds")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<MenuItemOrdered>, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            return await db.MenuItemOrdered.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is MenuItemOrdered model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetMenuItemOrderedById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, MenuItemOrdered menuItemOrdered, MyRestaurantApiContext db) =>
        {
            var affected = await db.MenuItemOrdered
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, menuItemOrdered.Id)
                  .SetProperty(m => m.MenuItemId, menuItemOrdered.MenuItemId)
                  .SetProperty(m => m.TogoOrderId, menuItemOrdered.TogoOrderId)
                  .SetProperty(m => m.Name, menuItemOrdered.Name)
                  .SetProperty(m => m.Price, menuItemOrdered.Price)
                  .SetProperty(m => m.Category, menuItemOrdered.Category)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateMenuItemOrdered")
        .WithOpenApi();

        group.MapPost("/", async (MenuItemOrdered menuItemOrdered, MyRestaurantApiContext db) =>
        {
            db.MenuItemOrdered.Add(menuItemOrdered);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/MenuItemOrdered/{menuItemOrdered.Id}",menuItemOrdered);
        })
        .WithName("CreateMenuItemOrdered")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            var affected = await db.MenuItemOrdered
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteMenuItemOrdered")
        .WithOpenApi();
    }
}
