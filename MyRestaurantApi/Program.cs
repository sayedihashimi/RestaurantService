using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRestaurantApi.Data;
using MyRestaurantApi;
using Microsoft.Extensions.FileProviders;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MyRestaurantApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MyRestaurantApiContext") ?? throw new InvalidOperationException("Connection string 'MyRestaurantApiContext' not found.")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAntiforgery();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
// uncommenting to see swagger UI page in prod, don't do this unless you are sure you want this.
/*if (app.Environment.IsDevelopment())*/ {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapContactEndpoints();

app.MapMenuItemEndpoints();

app.MapMenuItemOrderedEndpoints();

app.MapTogoOrderEndpoints();

app.MapFallbackToFile("/index.html");

app.Run();

