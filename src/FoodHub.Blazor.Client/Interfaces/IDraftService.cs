namespace FoodHub.Blazor.Client.Interfaces
{
    public interface IDraftService
    {

         Task SaveAsync<T>(string key, T data);

         Task<T?> LoadAsync<T>(string key);

         Task RemoveAsync(string key);

         Task<bool> ExistAsync(string key);
    }
}
