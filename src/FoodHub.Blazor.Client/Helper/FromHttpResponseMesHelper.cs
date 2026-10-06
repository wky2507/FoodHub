using System.Text.Json;

namespace FoodHub.Blazor.Client.Helper
{
    public static class FromHttpResponseMesHelper
    {
        public static async Task<T?> FromHttpResponseMes<T>(HttpResponseMessage response) where T: class
        {
            var json = await response.Content.ReadAsStringAsync();

            if (json == null) {

                return default;
            }

            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }


    }
}
