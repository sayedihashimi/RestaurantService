var builder = DistributedApplication.CreateBuilder(args);

var restaurantdb = builder.AddSqlite("restaurantdb")
                .WithSqliteWeb();

var apiService = builder.AddProject<Projects.MyRestaurantApi>("myrestaurantapi")
                    .WithReference(restaurantdb)
                    .WithHttpHealthCheck("/health");

builder.AddViteApp(name: "frontend", workingDirectory: "../restaurantweb")
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithNpmPackageInstallation();

builder.Build().Run();
