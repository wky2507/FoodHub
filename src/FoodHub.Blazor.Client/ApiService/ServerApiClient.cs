using System.Net.Http.Json;

namespace FoodHub.Blazor.Client.ApiService
{
    public class ServerApiClient
    {

        private readonly HttpClient _client;

        public ServerApiClient(HttpClient client) {

            _client = client;
        
        }

        public async Task<T?> GetFromJsonsync<T>(string url) {

            return await _client.GetFromJsonAsync<T>(url);
        
        }


    }
}
