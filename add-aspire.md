
Steps to add aspire support

1. Add `nuget.config` to root
   ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
    <packageSources>
        <clear />
        <add key="https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet9/nuget/v3/index.json" value="https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet9/nuget/v3/index.json" />
        <add key="https://api.nuget.org/v3/index.json" value="https://api.nuget.org/v3/index.json" />
        <add key="dotnet10" value="https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet10/nuget/v3/index.json" />
    </packageSources>
    <packageSourceMapping>
        <packageSource key="https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet9/nuget/v3/index.json">
        <package pattern="Aspire*" />
        <package pattern="Microsoft.Extensions.ServiceDiscovery*" />
        </packageSource>
        <packageSource key="https://api.nuget.org/v3/index.json">
        <package pattern="*" />
        </packageSource>
    </packageSourceMapping>
    </configuration>
   ```
2. Add a `Directory.Build.props` file
   ```xml
    <Project>
        <PropertyGroup>
            <HotReloadAutoRestart>true</HotReloadAutoRestart>
        </PropertyGroup>
    </Project>
   ```
3. Add AppHost + Service Defaults project with `aspire new`
4. Start the AppHost in VS or run `dotnet watch` on the AppHost
5. API: Remove the SpaProxy related properties from `MyRestaurantApi` project. This is needed to prevent the api project from starting the .esproj, Aspire will take care of that.
6. API: Remove PackageReference to `Microsoft.AspNetCore.SpaProxy`
7. API: Remove the Project Reference to `restaurantweb.esproj`.
8. AppHost: Add a reference to web api project. After changes are applied the web app should appear in the dashboard.
9.  AppHost.cs: Add after builder declaration (`var builder = ...`). After changes are applied the web app should appear in the dashboard.
    ```cs
    var apiService = builder.AddProject<Projects.MyRestaurantApi>("myrestaurantapi");
    ```
10. API: Add a reference to Service Defaults project
11. API.Program.cs: Add after builder declaration (`var builder = ...`).
    ```cs
    builder.AddServiceDefaults();
    builder.Services.AddProblemDetails();
    ```
12. API.Program.cs: Add after app declaration (`var app = ...`)
    ```cs
    app.MapDefaultEndpoints();
    ```
13. API.launchSettings.json: Remove all env var declarations for `ASPNETCORE_HOSTINGSTARTUPASSEMBLIES`.
14. AppHost: `aspire add sqlite`
15. AppHost.Program.cs: Add after builder declaration (`var builder = ...`), make sure it's before `var apiService`.
    ```cs
    var restaurantdb = builder.AddSqlite("restaurantdb")
                                .WithSqliteWeb();
    ```
16. AppHost.Program.cs: Update apiService declaration to the following.
    ```cs
    var apiService = builder.AddProject<Projects.MyRestaurantApi>("myrestaurantapi")
                    .WithReference(restaurantdb)
                    .WithHttpHealthCheck("/health");
    ```
17. API: Add reference to `CommunityToolkit.Aspire.Microsoft.EntityFrameworkCore.Sqlite`.
    ```bash
    dotnet add package CommunityToolkit.Aspire.Microsoft.EntityFrameworkCore.Sqlite
    ```
18. API.Program.cs: Remove the following code.
    ```cs
    builder.Services.AddDbContext<MyRestaurantApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MyRestaurantApiContext") ?? throw new InvalidOperationException("Connection string 'MyRestaurantApiContext' not found.")));
    ```
19. API.Program.cs: Add after `builder` declaration.
    ```cs
    builder.AddSqliteDbContext<MyRestaurantApiContext>("restaurantdb");
    ```
20. API.Program.cs: Add immediately before `app.Run()`
    ```cs
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<MyRestaurantApiContext>();
    await dbContext.Database.MigrateAsync();
    ```
21. AppHost: run `aspire add nodejs`
22. AppHost: run `aspire add communitytoolkit-nodejs-extensions` 
23. AppHost.cs: Add before `builder.Build().Run()`
    ```cs
    builder.AddViteApp(name: "frontend", workingDirectory: "../restaurantweb")
            .WithReference(apiService)
            .WaitFor(apiService)
            .WithNpmPackageInstallation();
    ```
24. JS.vite.config.js: Add import `import { defineConfig, loadEnv } from 'vite'`
25. JS.vite.config.js: Remove `const target = ...` declaration
26. JS.vite.config.js: Replace the entire `export` declaration with the following
    ```javascript
    export default defineConfig(({mode}) =>{
        const env = loadEnv(mode, process.cwd(), '');

        return {
            plugins: [plugin()],
            resolve: {
                alias: {
                    '@': fileURLToPath(new URL('./src', import.meta.url))
                }
            },
            server: {
                proxy: {
                    '/api': {
                        target: process.env.services__myrestaurantapi__https__0 || process.env.services__myrestaurantapi__http__0,
                        changeOrigin: true,
                        secure: false
                    }
                },
                port: parseInt(env.VITE_PORT)
            },
            build:{
                outDir: 'dist',
                rollupOptions: {
                    input: './index.html'
                }
            }
        }
    })
    ```
