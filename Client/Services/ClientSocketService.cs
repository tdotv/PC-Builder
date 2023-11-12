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
        // return await _httpClient.GetFromJsonAsync<List<Sockets>>("controller");

        var response = await _httpClient.GetAsync("api/sockets");
        if (response.IsSuccessStatusCode) { return await response.Content.ReadFromJsonAsync<List<Sockets>>(); }
        else { return new List<Sockets>(); }
    }
}