using PC_Designer.Shared;

public interface ISocketService
{
    Task<List<Sockets>?> GetSocketsAsync();
}