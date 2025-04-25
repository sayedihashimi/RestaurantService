using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

[McpServerToolType]
public class MenuItemTool
{
    private readonly HttpClient httpClient = new HttpClient();
    private readonly string baseUrl;

    public MenuItemTool(IConfiguration configuration)
    {
        baseUrl = configuration["Config:RestaurantApiBaseUrl"] ?? "http://localhost:5000";
    }

    [McpServerTool, Description("Get all menu items.")]
    public async Task<string> GetAllMenuItemsAsync()
    {
        var response = await httpClient.GetAsync($"{baseUrl}/api/MenuItem");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get all menu items by category.")]
    public async Task<string> GetMenuItemsByCategoryAsync(string category)
    {
        var response = await httpClient.GetAsync($"{baseUrl}/api/MenuItem/cat/{category}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get a menu item by its Id.")]
    public async Task<string> GetMenuItemByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"{baseUrl}/api/MenuItem/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Create a new menu item.")]
    public async Task<string> CreateMenuItemAsync(string menuItemJson)
    {
        var content = new StringContent(menuItemJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"{baseUrl}/api/MenuItem", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Update a menu item by Id.")]
    public async Task<string> UpdateMenuItemAsync(int id, string menuItemJson)
    {
        var content = new StringContent(menuItemJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync($"{baseUrl}/api/MenuItem/{id}", content);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Delete a menu item by Id.")]
    public async Task<bool> DeleteMenuItemAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"{baseUrl}/api/MenuItem/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return false;
        response.EnsureSuccessStatusCode();
        return true;
    }
}
