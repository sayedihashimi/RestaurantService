using ModelContextProtocol.Server;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

[McpServerToolType]
public static class MenuItemOrderedTool
{
    private static readonly HttpClient httpClient = new HttpClient();
    private static IConfiguration? _configuration;
    private static string? _baseUrl;

    public static void SetConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
        _baseUrl = _configuration["Config:RestaurantApiBaseUrl"] ?? "http://localhost:5000";
    }

    private static string BaseUrl => _baseUrl ?? "http://localhost:5000";

    [McpServerTool, Description("Get all menu items ordered.")]
    public static async Task<string> GetAllMenuItemOrderedsAsync()
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/api/MenuItemOrdered");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get a menu item ordered by its Id.")]
    public static async Task<string> GetMenuItemOrderedByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/api/MenuItemOrdered/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get menu items ordered by TogoOrder Id.")]
    public static async Task<string> GetMenuItemOrderedByTogoOrderIdAsync(int togoOrderId)
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/api/MenuItemOrdered/togoorder/{togoOrderId}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Create a new menu item ordered.")]
    public static async Task<string> CreateMenuItemOrderedAsync(string menuItemOrderedJson)
    {
        var content = new StringContent(menuItemOrderedJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"{BaseUrl}/api/MenuItemOrdered", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Update a menu item ordered by Id.")]
    public static async Task<string> UpdateMenuItemOrderedAsync(int id, string menuItemOrderedJson)
    {
        var content = new StringContent(menuItemOrderedJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync($"{BaseUrl}/api/MenuItemOrdered/{id}", content);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Delete a menu item ordered by Id.")]
    public static async Task<bool> DeleteMenuItemOrderedAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"{BaseUrl}/api/MenuItemOrdered/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return false;
        response.EnsureSuccessStatusCode();
        return true;
    }
}
