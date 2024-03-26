using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using MyRestaurantApi.Data;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
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
        .WithOpenApi(operation => new(operation) {
			Summary = "Gets all Menu Items"
		});

        group.MapGet("/cat/{category}",(MenuItemCategory category, MyRestaurantApiContext db) => {
            var menuItems = db.MenuItem.AsNoTracking();

            var catValue = (int)category;


            var catLower = category.ToString().ToLower();

            return db.MenuItem.Where(item => (int)item.Category! == catValue);
        })
        .WithName("GetAllMenuItemsByType")
        .WithOpenApi(operation => new(operation) {
	        Summary = "Gets all menu items for the given category",
	        Parameters = new List<OpenApiParameter>
	        {
		        new OpenApiParameter
		        {
			        Name = "category",
			        In = ParameterLocation.Path,
			        Required = true,
			        Schema = new OpenApiSchema
			        {
				        Type = "string",
				        Enum = new List<IOpenApiAny>
				        {
					        new OpenApiString("Breakfast"),
					        new OpenApiString("Lunch"),
					        new OpenApiString("Dinner"),
					        new OpenApiString("Drink"),
					        new OpenApiString("Side")
				        }
			        },
			        Description = "Category for the items to get. Must be a valid value for MenuItemCategory enum"
		        }
	        }
        });

        group.MapGet("/{id}", async Task<Results<Ok<MenuItem>, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            return await db.MenuItem.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is MenuItem model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetMenuItemById")
        .WithOpenApi(operation => new(operation) {
			Summary = "Get a MenuItem by it's Id",
			Parameters = new List<OpenApiParameter> {
				new OpenApiParameter {
					Name = "id",
					In = ParameterLocation.Path,
					Required = true,
					Schema = new OpenApiSchema {
						Type = "integer",
						Format = "int32"
					},
					Description = "Id of the MenuItem to get"
				}
			}
		});

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
        .WithOpenApi(operation => new(operation) {
			Summary = "Update a MenuItem by its id",
			Parameters = new List<OpenApiParameter>
	        {
		        new OpenApiParameter
		        {
			        Name = "id",
			        In = ParameterLocation.Path,
			        Required = true,
			        Schema = new OpenApiSchema
			        {
				        Type = "integer",
				        Format = "int32"
			        },
			        Description = "Id of the MenuItem to update"
		        }
	        }
		});

        group.MapPost("/", async (MenuItem menuItem, MyRestaurantApiContext db) =>
        {
            db.MenuItem.Add(menuItem);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/MenuItem/{menuItem.Id}",menuItem);
        })
        .WithName("CreateMenuItem")
        .WithOpenApi(operation => new(operation) {
			Summary = "Creates a new MenuItem"
		});

		group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, MyRestaurantApiContext db) =>
		{
			var affected = await db.MenuItem
				.Where(model => model.Id == id)
				.ExecuteDeleteAsync();

			return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
		})
		.WithName("DeleteMenuItem")
		.WithOpenApi(operation => new(operation) {
			Summary = "Deletes a MenuItem by its Id",
			Parameters = new List<OpenApiParameter>
			{
				new OpenApiParameter
				{
					Name = "id",
					In = ParameterLocation.Path,
					Required = true,
					Schema = new OpenApiSchema
					{
						Type = "integer",
						Format = "int32"
					},
					Description = "Id of the MenuItem to delete"
				}
			}
		});
    }
}
