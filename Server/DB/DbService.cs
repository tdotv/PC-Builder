using Dapper;
using System.Data.SqlClient;

public class DbService : IDbService
{
    private readonly string? _connectionString;

    public DbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("ConnectionString is null");
        if (_connectionString != null) { using var connection = new SqlConnection(_connectionString); }   
    }

    public async Task<T> GetAsync<T>(string command, object parms, SqlConnection connection)
    {
        await connection.OpenAsync();
        T result;
        result = (await connection.QueryAsync<T>(command, parms).ConfigureAwait(false)).FirstOrDefault() ?? throw new InvalidOperationException("Result is null");
        return result;
    }

//    public async Task<List<T>> GetAll<T>(string command, object parms)
//     {
//         List<T> result = new List<T>();
//         result = (await _db.QueryAsync<T>(command, parms)).ToList();
//         return result;
//     } 

//     public async Task<T> Insert<T>(string command, object parms)
//     {
//         T result;
//         result = _db.Query<T>(command, parms).FirstOrDefault();
//         return result;
//     }
 
//     public async Task<T> Update<T>(string command, object parms)
//     {
//         T result;
//         result = _db.Query<T>(command, parms).FirstOrDefault();
//         return result;
//     }

//     public async Task<T> Delete<T>(string command, object parms)
//     {
//         T result;
//         result = _db.Query<T>(command, parms).FirstOrDefault();
//         return result;
//     }
}