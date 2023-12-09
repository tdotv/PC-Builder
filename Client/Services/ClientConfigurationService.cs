using System.Net.Http.Json;
using PC_Designer.ViewModels;
using PC_Designer.Shared;

public class ClientConfigurationService : IPcConfigurationService
{
    private readonly HttpClient _httpClient;

    public ClientConfigurationService(HttpClient httpClient)
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

    public async Task<List<MotherBoards>?> GetMotherBoardsAsync()
    {
        var response = await _httpClient.GetAsync("api/motherboard");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<List<MotherBoards>>()
            : null;
    }

    public async Task<List<CPUs>?> GetCpusAsync()
    {
        var response = await _httpClient.GetAsync("api/cpu");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<List<CPUs>>()
            : null;
    }

    public async Task<List<GraphicalCards>?> GetGraphicalCardsAsync()
    {
        var response = await _httpClient.GetAsync("api/graphicalcard");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<List<GraphicalCards>>()
            : null;
    }
    
    public async Task<List<ComputerCases>?> GetComputerCasesAsync()
    {
        var response = await _httpClient.GetAsync("api/computercase");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<List<ComputerCases>>()
            : null;
    }

    public async Task<int> CreateConfigurationsAsync(PcConfigurationViewModel configurationViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("api/configuration", configurationViewModel);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<int>()
            : 0;
    }

    public async Task<bool> DeleteConfigurationsAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/configuration/{id}");
        return response.IsSuccessStatusCode;
    }
}