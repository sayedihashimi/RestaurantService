This is a sample ASP.NET Core application that uses Aspire.

The app consists of the Aspire AppHost along with a web api and a front-end React app.

## Getting started

To get started with this project locally follow the steps outlined below.

### Visual Studio
1. Open the solution `MyRestaurantApi.sln` in Visual Studio 2022 17.14 or later.
1. Set the startup project to be `MyRestaurantApi.AppHost`.
1. `F5` or `CTRL-F5`

### CLI
1. `cd` to the `MyRestaurantApi.AppHost` directory
1. `aspire run` or `dotnet watch`

## Additional Resources

- https://learn.microsoft.com/en-us/aspnet/core/test/http-files?view=aspnetcore-8.0
- https://devblogs.microsoft.com/visualstudio/web-api-development-in-visual-studio-2022/
- https://devblogs.microsoft.com/visualstudio/safely-use-secrets-in-http-requests-in-visual-studio-2022/
- https://learn.microsoft.com/en-us/connectors/custom-connectors/port-tunneling
