using PC_Designer.Shared;

public class ServerSocketService : ISocketService
{
    private readonly IDbService _dbService;

    public ServerSocketService(IDbService dbService) { _dbService = dbService ;}

    public async Task<List<Sockets>?> GetSocketsAsync() 
    {
        return await _dbService.GetAll<Sockets>("SELECT * FROM dbo.Sockets", new { }); 
    }

    public async Task<bool> CreateSocketsAsync(Sockets sockets)
    {
        await _dbService.Insert<int>("INSERT INTO dbo.Sockets (SocketId, Name) VALUES (@SocketId, @Name)", sockets);
        return true;
    }

    public async Task<Sockets?> UpdateSocketsAsync(Sockets sockets)
    {
        await _dbService.Update<int>("UPDATE dbo.Sockets SET name=@Name WHERE SocketId=@SocketId", sockets);
        return await _dbService.GetAsync<Sockets>("SELECT * From dbo.Sockets WHERE SocketId=@SocketId", new { SocketId = sockets.SocketId });
    }

    public async Task<bool> DeleteSocketsAsync(int key)
    {
        await _dbService.Delete<int>("DELETE FROM dbo.Sockets WHERE SocketId=@SocketId", new { id = key });
        return true;
    }

}