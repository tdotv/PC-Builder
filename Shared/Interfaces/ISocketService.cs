using PC_Designer.ViewModels;

public interface ISocketService
{
    Task<List<SocketsViewModel>?> GetSocketsAsync();
    Task<bool> CreateSocketsAsync(SocketsViewModel socketsViewModel);
    Task<SocketsViewModel?> UpdateSocketsAsync(SocketsViewModel socketsViewModel);
    Task<bool> DeleteSocketsAsync(int key);
}