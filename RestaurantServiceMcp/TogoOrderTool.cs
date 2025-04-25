using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

[McpServerToolType]
public class TogoOrderTool
{
    private readonly HttpClient httpClient = new HttpClient();
    private readonly string baseUrl;

    public TogoOrderTool(IConfiguration configuration)
    {
        baseUrl = configuration["Config:RestaurantApiBaseUrl"] ?? "http://localhost:5000";
    }

    [McpServerTool, Description("Get all togo orders.")]
    public async Task<string> GetAllTogoOrdersAsync()
    {
        var response = await httpClient.GetAsync($"{baseUrl}/api/TogoOrder");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get a togo order by its Id.")]
    public async Task<string?> GetTogoOrderByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"{baseUrl}/api/TogoOrder/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Create a new togo order.")]
    public async Task<string> CreateTogoOrderAsync(string togoOrderJson)
    {
        var content = new StringContent(togoOrderJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"{baseUrl}/api/TogoOrder", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Update a togo order by Id.")]
    public async Task<string?> UpdateTogoOrderAsync(int id, string togoOrderJson)
    {
        var content = new StringContent(togoOrderJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync($"{baseUrl}/api/TogoOrder/{id}", content);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Delete a togo order by Id.")]
    public async Task<bool> DeleteTogoOrderAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"{baseUrl}/api/TogoOrder/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return false;
        response.EnsureSuccessStatusCode();
        return true;
    }

    [McpServerTool, Description("Create a new togo order from form data.")]
    public async Task<string> CreateTogoOrderFromFormAsync(string formData, string contentType)
    {
        var content = new StringContent(formData, Encoding.UTF8, contentType); // contentType: "application/x-www-form-urlencoded" or "multipart/form-data"
        var response = await httpClient.PostAsync($"{baseUrl}/api/TogoOrder/form", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
