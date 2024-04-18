using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using MyRestaurantApi.Data;
using Microsoft.OpenApi.Models;
namespace MyRestaurantApi;

public static class TogoOrderEndpoints
{
    public static void MapTogoOrderEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/TogoOrder").WithTags(nameof(TogoOrder));

        group.MapGet("/", async (MyRestaurantApiContext db) =>
        {
            return await db.TogoOrder
                .Include(order => order.ItemsOrdered)
                .ToListAsync();
        })
        .WithName("GetAllTogoOrders")
        .WithOpenApi(operation => new(operation) { 
            Summary = "Creates a new togo order" 
        });

        group.MapGet("/{id}", async Task<Results<Ok<TogoOrder>, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            return await db.TogoOrder.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is TogoOrder model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetTogoOrderById")
        .WithOpenApi(operation => new(operation) {
			Summary = "Get a togo order by it's Id",
			Parameters = new List<OpenApiParameter> {
				new OpenApiParameter {
					Name = "id",
					In = ParameterLocation.Path,
					Required = true,
					Schema = new OpenApiSchema {
						Type = "integer",
						Format = "int32"
					},
					Description = "Id of the togo order to get"
				}
			}
		});

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
        .WithOpenApi(operation => new(operation) {
			Summary = "Updates the togo order by it's Id",
			Parameters = new List<OpenApiParameter> {
				new OpenApiParameter {
					Name = "id",
					In = ParameterLocation.Path,
					Required = true,
					Schema = new OpenApiSchema {
						Type = "integer",
						Format = "int32"
					},
					Description = "Id of the togo order to update"
				}
			}
		});

        group.MapPost("/", async (TogoOrder togoOrder, MyRestaurantApiContext db) =>
        {
            togoOrder.Customer = await db.Contact.FindAsync(togoOrder.Customer!.Id);

			togoOrder.OrderCreated ??= DateTime.Now;

			if (togoOrder.ItemsOrdered?.Count == 0) {
                foreach (var item in togoOrder.ItemsOrdered) {
                    var menuItem = await db.MenuItem.FindAsync(item.MenuItemId);
                    item.Name = menuItem!.Name;
                    if (item.Price is null || !item.Price.HasValue || item.Price.Value < 0) {
                        item.Price = menuItem.Price!.Value;
                    }
                    if (item.Category is null || !item.Category.HasValue) {
                        item.Category = menuItem.Category!.Value;
                    }
                }
            }

            db.TogoOrder.Add(togoOrder);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/TogoOrder/{togoOrder.Id}",togoOrder);
        })
        .WithName("CreateTogoOrder")
        .WithOpenApi(operation => new(operation) {
			Summary = "Creates a new togo order"
		});

        // added this just as a sample of getting the forms data to work, not needed otherwise
		group.MapPost("/form", async ([Microsoft.AspNetCore.Mvc.FromForm]TogoOrder togoOrder, MyRestaurantApiContext db) => {
			togoOrder.Customer = await db.Contact.FindAsync(togoOrder.Customer!.Id);

			if (togoOrder.OrderCreated == null) {
				togoOrder.OrderCreated = DateTime.Now;
			}
			if (togoOrder.ItemsOrdered != null && togoOrder.ItemsOrdered.Count > 0) {
				foreach (var item in togoOrder.ItemsOrdered) {
					var menuItem = await db.MenuItem.FindAsync(item.MenuItemId);
					item.Name = menuItem!.Name;
					if (item.Price is null || !item.Price.HasValue || item.Price.Value < 0) {
						item.Price = menuItem.Price!.Value;
					}
					if (item.Category is null || !item.Category.HasValue) {
						item.Category = menuItem.Category!.Value;
					}
				}
			}
			db.TogoOrder.Add(togoOrder);
			await db.SaveChangesAsync();
			return TypedResults.Created($"/api/TogoOrder/form/{togoOrder.Id}", togoOrder);
		})
		.WithName("CreateTogoOrderFromPost")
        .DisableAntiforgery()   // just for testing, don't add to your actual code unless you are sure about this!
		.WithOpenApi(operation => new(operation) {
			Summary = "Endpoint to update the togo order using a form"
		});

		group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            var affected = await db.TogoOrder
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteTogoOrder")
        .WithOpenApi(operation => new(operation) { 
            Summary = "Deletes the togo order with the given Id" 
        });
    }
}
