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

    // public async Task<int> CreateConfigurationsAsync(PcConfigurationViewModel configurationViewModel)
    // {
    //     var response = await _httpClient.PostAsJsonAsync("api/configuration", configurationViewModel);
    //     return response.IsSuccessStatusCode
    //         ? await response.Content.ReadFromJsonAsync<int>()
    //         : 0;
    // }

    // public async Task<bool> UpdateConfigurationAsync(PcConfigurations configuration)
    // {
    //     var response = await _httpClient.PutAsJsonAsync($"api/configuration/{configuration.PcConfigurationId}", configuration);
    //     return response.IsSuccessStatusCode;
    // }

    public async Task<bool> DeleteConfigurationsAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/configuration/{id}");
        return response.IsSuccessStatusCode;
    }
}