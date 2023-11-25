using System.Data;

using System.Data.SqlClient;

using PC_Designer.Shared;

public interface IDbService
{
  Task<T> GetAsync<T>(string command, object parms);
  Task<List<T>> GetAll<T>(string command, object parms);
  Task<T> Insert<T>(string command, object parms);
  Task<T> Update<T>(string command, object parms);
  Task<T> Delete<T>(string command, object parms);
}