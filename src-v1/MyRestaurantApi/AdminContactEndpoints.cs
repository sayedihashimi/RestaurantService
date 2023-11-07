using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using MyRestaurantApi.Data;
namespace MyRestaurantApi;

public static class AdminContactEndpoints
{
    public static void MapAdminContactEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/AdminContact").WithTags(nameof(AdminContact));

        group.MapGet("/", async (MyRestaurantApiContext db) =>
        {
            return await db.AdminContact.ToListAsync();
        })
        .WithName("GetAllAdminContacts")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<AdminContact>, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            return await db.AdminContact.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is AdminContact model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetAdminContactById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, AdminContact adminContact, MyRestaurantApiContext db) =>
        {
            var affected = await db.AdminContact
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, adminContact.Id)
                  .SetProperty(m => m.Name, adminContact.Name)
                  .SetProperty(m => m.Email, adminContact.Email)
                  .SetProperty(m => m.Phone, adminContact.Phone)
                );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateAdminContact")
        .WithOpenApi();

        group.MapPost("/", async (AdminContact adminContact, MyRestaurantApiContext db) =>
        {
            db.AdminContact.Add(adminContact);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/AdminContact/{adminContact.Id}",adminContact);
        })
        .WithName("CreateAdminContact")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, MyRestaurantApiContext db) =>
        {
            var affected = await db.AdminContact
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteAdminContact")
        .WithOpenApi();
    }
}
