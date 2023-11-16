using System.Net.Http.Json;
using PC_Designer.ViewModels;

public class ClientSocketService : ISocketService
{
    private readonly HttpClient _httpClient;

    public ClientSocketService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<SocketsViewModel>?> GetSocketsAsync()
    {
        var response = await _httpClient.GetAsync("api/sockets");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<List<SocketsViewModel>>()
            : null;
    }

    public async Task<bool> CreateSocketsAsync(SocketsViewModel socketsViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync("api/sockets", socketsViewModel);
        return response.IsSuccessStatusCode;
    }

    public async Task<SocketsViewModel?> UpdateSocketsAsync(SocketsViewModel socketsViewModel)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/sockets/{socketsViewModel.SocketId}", socketsViewModel);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<SocketsViewModel>();
        }
        return null;
    }

    public async Task<bool> DeleteSocketsAsync(int key)
    {
        var response = await _httpClient.DeleteAsync($"api/sockets/{key}");
        return response.IsSuccessStatusCode;
    }
}