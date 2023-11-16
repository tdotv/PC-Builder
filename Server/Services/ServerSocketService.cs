using PC_Designer.Shared;
using PC_Designer.ViewModels;

public class ServerSocketService : ISocketService
{
    private readonly IDbService _dbService;

    public ServerSocketService(IDbService dbService) { _dbService = dbService ;}

    public async Task<List<SocketsViewModel>?> GetSocketsAsync() 
    {
        var sockets = await _dbService.GetAll<Sockets>("SELECT * FROM dbo.Sockets", new { }); 
        return sockets.Select(socket => (SocketsViewModel)socket).ToList(); 
    }

    public async Task<bool> CreateSocketsAsync(SocketsViewModel socketsViewModel)
    {
        await _dbService.Insert<int>("INSERT INTO dbo.Sockets (SocketId, Name) VALUES (@SocketId, @Name)", socketsViewModel);
        return true;
    }

    public async Task<SocketsViewModel?> UpdateSocketsAsync(SocketsViewModel socketsViewModel)
    {
        await _dbService.Update<int>("UPDATE dbo.Sockets SET name=@Name WHERE SocketId=@SocketId", socketsViewModel);
        return await _dbService.GetAsync<Sockets>("SELECT * From dbo.Sockets WHERE SocketId=@SocketId", new { SocketId = socketsViewModel.SocketId });
    }

    public async Task<bool> DeleteSocketsAsync(int key)
    {
        await _dbService.Delete<int>("DELETE FROM dbo.Sockets WHERE SocketId=@SocketId", new { id = key });
        return true;
    }

}