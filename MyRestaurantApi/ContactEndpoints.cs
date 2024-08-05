using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using MyRestaurantApi.Data;
using Microsoft.OpenApi.Models;
namespace MyRestaurantApi;

public static class ContactEndpoints
{
    public static void MapContactEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Contact").WithTags(nameof(Contact));

        group.MapGet("/", async (MyRestaurantApiContext db) => {
            return await db.Contact.ToListAsync();
        })
        .WithName("GetAllContacts")
        .WithOpenApi(operation => new(operation) {
            Summary = "Get all contacts"
        });

        
        group.MapGet("/{id}", async Task<Results<Ok<Contact>, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            return await db.Contact.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Contact model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetContactById")
        .WithOpenApi(operation => new(operation) {
	        Summary = "Get a contact by it's Id",
            Parameters = new List<OpenApiParameter> {
                new OpenApiParameter {
                    Name = "id",
                    In = ParameterLocation.Path,
                    Required = true,
                    Schema = new OpenApiSchema {
                        Type = "integer",
                        Format = "int32"
                    },
                    Description = "Id of the contact to get"
                }
            }
        });

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Contact contact, MyRestaurantApiContext db) =>
        {
            var affected = await db.Contact
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, id)
                  .SetProperty(m => m.Email, e => !string.IsNullOrEmpty(contact.Email) ? contact.Email : e.Email)
                  .SetProperty(m => m.Phone, e => !string.IsNullOrEmpty(contact.Phone) ? contact.Phone : e.Phone)
                  .SetProperty(m=>m.Name, e=> !string.IsNullOrEmpty(contact.Name) ? contact.Name : e.Name)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateContact")
        .WithOpenApi(operation => new(operation) {
			Summary = "Update a contact (specified by Id)",
            Description = "Updates a contact with the new values provided. You must specify the Id of the contact which should be updated."
		});

        group.MapPost("/", async (Contact contact, MyRestaurantApiContext db) =>
        {
            db.Contact.Add(contact);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Contact/{contact.Id}",contact);
        })
        .WithName("CreateContact")
        .WithOpenApi(operation => new(operation) {
			Summary = "Create a new contact",
            Description = "Creates a new contact. The Id field will automatically be set when the object is persisted to the database."
		});

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            var affected = await db.Contact
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteContact")
        .WithOpenApi(operation => new(operation) {
			Summary = "Deletes the specified contact"
		});
    }
}
