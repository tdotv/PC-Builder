using System.Net.Http.Json;
using PC_Designer.Shared;

public class ClientSocketService : ISocketService
{
    private readonly HttpClient _httpClient;

    public ClientSocketService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Sockets>?> GetSocketsAsync()
    {
        var response = await _httpClient.GetAsync("api/sockets");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<List<Sockets>>()
            : null;
    }

    public async Task<bool> CreateSocketsAsync(Sockets sockets)
    {
        var response = await _httpClient.PostAsJsonAsync("api/sockets", sockets);
        return response.IsSuccessStatusCode;
    }

    public async Task<Sockets?> UpdateSocketsAsync(Sockets sockets)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/sockets/{sockets.SocketId}", sockets);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Sockets>();
        }
        return null;
    }

    public async Task<bool> DeleteSocketsAsync(int key)
    {
        var response = await _httpClient.DeleteAsync($"api/sockets/{key}");
        return response.IsSuccessStatusCode;
    }
}