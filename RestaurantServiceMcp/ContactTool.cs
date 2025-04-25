using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

[McpServerToolType]
public class ContactTool
{
    private readonly HttpClient httpClient = new HttpClient();
    private readonly string baseUrl;

    public ContactTool(IConfiguration configuration)
    {
        baseUrl = configuration["Config:RestaurantApiBaseUrl"] ?? "http://localhost:5192";
    }

    [McpServerTool, Description("Get all contacts.")]
    public async Task<string> GetAllContactsAsync()
    {
        var response = await httpClient.GetAsync($"{baseUrl}/api/Contact");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Get a contact by its Id.")]
    public async Task<string?> GetContactByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"{baseUrl}/api/Contact/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Create a new contact.")]
    public async Task<string> CreateContactAsync(string contactJson)
    {
        var content = new StringContent(contactJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"{baseUrl}/api/Contact", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Update a contact by Id.")]
    public async Task<string?> UpdateContactAsync(int id, string contactJson)
    {
        var content = new StringContent(contactJson, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync($"{baseUrl}/api/Contact/{id}", content);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    [McpServerTool, Description("Delete a contact by Id.")]
    public async Task<bool> DeleteContactAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"{baseUrl}/api/Contact/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
        response.EnsureSuccessStatusCode();
        return true;
    }
}
