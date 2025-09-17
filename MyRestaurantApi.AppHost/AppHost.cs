var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddSqlite("restaurant-db")
                .WithSqliteWeb();

// var apiService = builder.AddProject<Projects.MyRestaurantApi>("myrestaurantapi")
//                     .WithHttpHealthCheck("/health");

builder.AddViteApp(name: "frontend", workingDirectory: "../restaurantweb")
    // .WithReference(apiService)
    // .WaitFor(apiService)
    .WithNpmPackageInstallation();

builder.Build().Run();
