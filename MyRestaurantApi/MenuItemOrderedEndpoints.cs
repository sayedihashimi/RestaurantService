using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using MyRestaurantApi.Data;
using Microsoft.OpenApi.Models;
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
        .WithOpenApi(operation => new(operation) {
			Summary = "Gets all menu items ordered."
		});

        group.MapGet("/{id}", async Task<Results<Ok<MenuItemOrdered>, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            return await db.MenuItemOrdered.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is MenuItemOrdered model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetMenuItemOrderedById")
        .WithOpenApi(operation => new(operation) {
			Summary = "Get a menu item by it's Id",
			Parameters = new List<OpenApiParameter> {
				new OpenApiParameter {
					Name = "id",
					In = ParameterLocation.Path,
					Required = true,
					Schema = new OpenApiSchema {
						Type = "integer",
						Format = "int32"
					},
					Description = "Id of the menu item to get"
				}
			}
		});

		group.MapGet("/togoorder/{togoOrderId}", async Task<Results<Ok<List<MenuItemOrdered>>, NotFound>> (int togoOrderId, MyRestaurantApiContext db) => {
			var items = await db.MenuItemOrdered.AsNoTracking()
				.Where(item => item.TogoOrderId == togoOrderId)
				.ToListAsync();

			return items.Any()
				? TypedResults.Ok(items)
				: TypedResults.NotFound();
		})
        .WithName("GetMenuItemOrderedByTogoOrderId");


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
        .WithOpenApi(operation => new(operation) {
			Summary = "Updates a menu item by it's Id",
			Parameters = new List<OpenApiParameter> {
				new OpenApiParameter {
					Name = "id",
					In = ParameterLocation.Path,
					Required = true,
					Schema = new OpenApiSchema {
						Type = "integer",
						Format = "int32"
					},
					Description = "Id of the menu item to update"
				}
			}
		});

        group.MapPost("/", async (MenuItemOrdered menuItemOrdered, MyRestaurantApiContext db) =>
        {
            db.MenuItemOrdered.Add(menuItemOrdered);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/MenuItemOrdered/{menuItemOrdered.Id}",menuItemOrdered);
        })
        .WithName("CreateMenuItemOrdered")
        .WithOpenApi(operation => new(operation) {
			Summary = "Creates a new menu item"
		});

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            var affected = await db.MenuItemOrdered
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteMenuItemOrdered")
        .WithOpenApi(operation => new(operation) {
			Summary = "Deletes the menu item ordered by its Id"
		});
    }
}
