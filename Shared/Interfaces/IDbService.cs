using System.Data;

using System.Data.SqlClient;

public interface IDbService
{
  Task<T> GetAsync<T>(string command, object parms, SqlConnection connection);
//   Task<List<T>> GetAll<T>(string command, object parms);
//   Task<T> Insert<T>(string command, object parms);
//   Task<T> Update<T>(string command, object parms);
//   Task<T> Delete<T>(string command, object parms);
}