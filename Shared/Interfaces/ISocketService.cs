using PC_Designer.Shared;

public interface ISocketService
{
    Task<List<Sockets>?> GetSocketsAsync();
    Task<bool> CreateSocketsAsync(Sockets sockets);
    Task<Sockets?> UpdateSocketsAsync(Sockets sockets);
    Task<bool> DeleteSocketsAsync(int key);
}