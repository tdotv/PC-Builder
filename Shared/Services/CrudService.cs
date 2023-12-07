using PC_Designer.ViewModels;

public class CrudService<TModel, TKey> //   : ICrudService<TModel, TKey>
{
    private readonly IDbService dbService;
    
    public CrudService(IDbService dbService)
    {
        this.dbService = dbService;
    }

    public async Task<List<TModel>?> GetAsync()
    {
        var tableName = typeof(TModel).Name + "s";
        var query = $"SELECT * FROM dbo.{tableName}";
        var items = await dbService.GetAll<TModel>(query, new {});
        return items;
    }

    public async Task<bool> CreateAsync(TModel model)
    {
        var tableName = typeof(TModel).Name + "s";
        var query = $"INSERT INTO dbo.{tableName} VALUES (@{CrudService<TModel, TKey>.GetParamNames(model)})";
        await dbService.Insert<TKey>(query, model);
        return true;
    }

    public async Task<TModel?> UpdateAsync(TModel model)
    {
        var tableName = typeof(TModel).Name + "s";
        var keyName = CrudService<TModel, TKey>.GetKeyName(model);
        var query = $"UPDATE dbo.{tableName} SET {CrudService<TModel, TKey>.GetUpdateFields(model)} WHERE {keyName}=@{keyName}";
        await dbService.Update<TKey>(query, model);
        return await dbService.GetAsync<TModel>($"SELECT * FROM dbo.{tableName} WHERE {keyName}=@{keyName}", new { Key = model.GetType().GetProperty(keyName)?.GetValue(model) });
    }

    public async Task<bool> DeleteAsync(TKey key)
    {
        var tableName = typeof(TModel).Name + "s";
        var keyName = "Id";
        var query = $"DELETE FROM dbo.{tableName} WHERE {keyName}=@{keyName}";
        await dbService.Delete<TKey>(query, new { Id = key });
        return true;
    }

    private static string GetParamNames(TModel model)
    {
        var properties = model.GetType().GetProperties();
        return string.Join(", ", properties.Select(prop => prop.Name));
    }

    private static string GetUpdateFields(TModel model)
    {
        var properties = model.GetType().GetProperties().Where(prop => !prop.Name.Equals("Id")); // Исключаем Id из обновляемых полей
        return string.Join(", ", properties.Select(prop => $"{prop.Name}=@{prop.Name}"));
    }

    private static string GetKeyName(TModel model)
    {
        return "Id";
    }
}