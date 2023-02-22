using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantService.Api.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RestaurantServiceApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("RestaurantServiceApiContext") ?? throw new InvalidOperationException("Connection string 'RestaurantServiceApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
