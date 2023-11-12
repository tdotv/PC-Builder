using PC_Designer.Shared;

public class SocketService : ISocketService
{
    private readonly DbService _dbService;

    public SocketService(DbService dbService) { _dbService = dbService ;}

    public async Task<List<Sockets>?> GetSocketsAsync() { return await _dbService.GetSocketsAsync(); }
}