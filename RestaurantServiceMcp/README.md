Basic MCP server for the RestaurantService

To use in VS Code add the following to VS Code settings in the MCP object.

```json
"RestaurantServiceMCP":{
    "type":"stdio",
    "command": "dotnet",
    "args": [
        "run",
        "--project",
        "C:\\data\\mycode\\restaurantservice\\RestaurantServiceMcp\\RestaurantServiceMcp.csproj"
    ],
    "env": {}
}
```