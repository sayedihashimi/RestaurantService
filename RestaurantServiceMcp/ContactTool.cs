using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

[McpServerToolType]
public static class ContactTool
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
    private static Settings? _settings;

    [McpServerTool, Description("Get all contacts.")]
    public static async Task<string> GetAllContactsAsync()
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/api/Contact");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get a contact by its Id.")]
    public static async Task<string?> GetContactByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"{BaseUrl}/api/Contact/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Create a new contact.")]
    public static async Task<string> CreateContactAsync(string contactJson)
    {
        var content = new StringContent(contactJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"{BaseUrl}/api/Contact", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Update a contact by Id.")]
    public static async Task<string?> UpdateContactAsync(int id, string contactJson)
    {
        var content = new StringContent(contactJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync($"{BaseUrl}/api/Contact/{id}", content);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Delete a contact by Id.")]
    public static async Task<bool> DeleteContactAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"{BaseUrl}/api/Contact/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
        response.EnsureSuccessStatusCode();
        return true;
    }
}
