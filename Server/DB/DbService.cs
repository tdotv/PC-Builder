using System.Data.SqlClient;
using Dapper;
using PC_Designer.Shared;

public class DbService
{
    private readonly string _connectionString;

    public DbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("ConnectionString is null");
    }

    public async Task<List<Sockets>> GetSocketsAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = "SELECT SocketId, Name FROM Sockets";
        var result = await connection.QueryAsync<Sockets>(query);

        return result.ToList();
    }
}