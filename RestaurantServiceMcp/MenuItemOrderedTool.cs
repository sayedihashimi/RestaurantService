using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

[McpServerToolType]
public class MenuItemOrderedTool
{
    private readonly HttpClient httpClient = new HttpClient();
    private readonly string baseUrl;

    public MenuItemOrderedTool(IConfiguration configuration)
    {
        baseUrl = configuration["Config:RestaurantApiBaseUrl"] ?? "http://localhost:5000";
    }

    [McpServerTool, Description("Get all menu items ordered.")]
    public async Task<string> GetAllMenuItemOrderedsAsync()
    {
        var response = await httpClient.GetAsync($"{baseUrl}/api/MenuItemOrdered");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get a menu item ordered by its Id.")]
    public async Task<string?> GetMenuItemOrderedByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"{baseUrl}/api/MenuItemOrdered/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get menu items ordered by TogoOrder Id.")]
    public async Task<string?> GetMenuItemOrderedByTogoOrderIdAsync(int togoOrderId)
    {
        var response = await httpClient.GetAsync($"{baseUrl}/api/MenuItemOrdered/togoorder/{togoOrderId}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Create a new menu item ordered.")]
    public async Task<string> CreateMenuItemOrderedAsync(string menuItemOrderedJson)
    {
        var content = new StringContent(menuItemOrderedJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"{baseUrl}/api/MenuItemOrdered", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Update a menu item ordered by Id.")]
    public async Task<string?> UpdateMenuItemOrderedAsync(int id, string menuItemOrderedJson)
    {
        var content = new StringContent(menuItemOrderedJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync($"{baseUrl}/api/MenuItemOrdered/{id}", content);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Delete a menu item ordered by Id.")]
    public async Task<bool> DeleteMenuItemOrderedAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"{baseUrl}/api/MenuItemOrdered/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return false;
        response.EnsureSuccessStatusCode();
        return true;
    }
}
