var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MyRestaurantApi>("myrestaurantapi");

builder.Build().Run();
