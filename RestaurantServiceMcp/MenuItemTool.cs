using Microsoft.Extensions.Configuration;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

[McpServerToolType]
public static class MenuItemTool
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

    [McpServerTool, Description("Get all menu items.")]
    public static async Task<string> GetAllMenuItemsAsync()
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/api/MenuItem");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get all menu items by category.")]
    public static async Task<string> GetMenuItemsByCategoryAsync(string category)
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/api/MenuItem/cat/{category}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get a menu item by its Id.")]
    public static async Task<string> GetMenuItemByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/api/MenuItem/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Create a new menu item.")]
    public static async Task<string> CreateMenuItemAsync(string menuItemJson)
    {
        var content = new StringContent(menuItemJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"{BaseUrl}/api/MenuItem", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Update a menu item by Id.")]
    public static async Task<string> UpdateMenuItemAsync(int id, string menuItemJson)
    {
        var content = new StringContent(menuItemJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync($"{BaseUrl}/api/MenuItem/{id}", content);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Delete a menu item by Id.")]
    public static async Task<bool> DeleteMenuItemAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"{BaseUrl}/api/MenuItem/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return false;
        response.EnsureSuccessStatusCode();
        return true;
    }
}
