var builder = DistributedApplication.CreateBuilder(args);

var restaurantdb = builder.AddSqlite("restaurantdb")
                            .WithSqliteWeb();

var apiService = builder.AddProject<Projects.MyRestaurantApi>("myrestaurantapi")
                .WithReference(restaurantdb)
                .WithHttpHealthCheck("/health");

builder.Build().Run();
