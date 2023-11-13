using Dapper;
using System.Data.SqlClient;

public class DbService : IDbService
{
    // private readonly IDbConnection? _db;
    private readonly string? _connectionString;

    public DbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("ConnectionString is null");
        // if (_connectionString != null) { _db = new SqlConnection(_connectionString); }   
    }

    private async Task<List<T>> ExecuteQueryAsync<T>(string command, object parms)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync().ConfigureAwait(false);
        return (await connection.QueryAsync<T>(command, parms).ConfigureAwait(false)).ToList();
    }

    public async Task<T> GetAsync<T>(string command, object parms)
    {
        return (await ExecuteQueryAsync<T>(command, parms)).FirstOrDefault()!;
    }

   public async Task<List<T>> GetAll<T>(string command, object parms)
    {
        return await ExecuteQueryAsync<T>(command, parms);
    } 

    public async Task<T> Insert<T>(string command, object parms)
    {
        return (await ExecuteQueryAsync<T>(command, parms)).FirstOrDefault()!;
    }
 
    public async Task<T> Update<T>(string command, object parms)
    {
        return (await ExecuteQueryAsync<T>(command, parms)).FirstOrDefault()!;
    }

    public async Task<T> Delete<T>(string command, object parms)
    {
        return (await ExecuteQueryAsync<T>(command, parms)).FirstOrDefault()!;
    }
}