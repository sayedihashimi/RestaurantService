using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using MyRestaurantApi.Data;
namespace MyRestaurantApi;

public static class MenuItemEndpoints
{
    public static void MapMenuItemEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/MenuItem").WithTags(nameof(MenuItem));

        group.MapGet("/", async (MyRestaurantApiContext db) =>
        {
            return await db.MenuItem.ToListAsync();
        })
        .WithName("GetAllMenuItems")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<MenuItem>, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            return await db.MenuItem.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is MenuItem model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetMenuItemById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, MenuItem menuItem, MyRestaurantApiContext db) =>
        {
            var affected = await db.MenuItem
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, menuItem.Id)
                  .SetProperty(m => m.Name, menuItem.Name)
                  .SetProperty(m => m.Price, menuItem.Price)
                  .SetProperty(m => m.Description, menuItem.Description)
                  .SetProperty(m => m.Category, menuItem.Category)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateMenuItem")
        .WithOpenApi();

        group.MapPost("/", async (MenuItem menuItem, MyRestaurantApiContext db) =>
        {
            db.MenuItem.Add(menuItem);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/MenuItem/{menuItem.Id}",menuItem);
        })
        .WithName("CreateMenuItem")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            var affected = await db.MenuItem
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteMenuItem")
        .WithOpenApi();
    }
}
