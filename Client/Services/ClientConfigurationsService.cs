using System.Net.Http.Json;
using PC_Designer.ViewModels;

public class ClientConfigurationsService : IPcConfigurationService
{
    private readonly HttpClient _httpClient;

    public ClientConfigurationsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PcConfigurationViewModel>?> GetConfigurationsAsync()
    {
        var response = await _httpClient.GetAsync("api/configuration");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<List<PcConfigurationViewModel>>()
            : null;
    }

    public async Task<bool> DeleteConfigurationsAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/configuration/{id}");
        return response.IsSuccessStatusCode;
    }
}