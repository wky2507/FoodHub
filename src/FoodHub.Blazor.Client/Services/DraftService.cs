using Blazored.LocalStorage;
using FoodHub.Blazor.Client.Interfaces;
namespace FoodHub.Blazor.Client.Services
{
    public class DraftService : IDraftService
    {   

        private readonly ILocalStorageService _localStorageService;


        public DraftService(ILocalStorageService localStorageService) {

            _localStorageService = localStorageService;

        }


       public async Task SaveAsync<T>(string key, T data) {

            await _localStorageService.SetItemAsync(key, data);
        
        }

       public async Task<T?> LoadAsync<T>(string key) {

           return await _localStorageService.GetItemAsync<T>(key);
        
        }

       public async Task RemoveAsync(string key) {

           await _localStorageService.RemoveItemAsync(key);
        }

       public async Task<bool> ExistAsync(string key) {

           return await _localStorageService.ContainKeyAsync(key);

        }

    }
}
