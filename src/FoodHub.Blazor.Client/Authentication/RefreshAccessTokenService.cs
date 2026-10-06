using FoodHub.BlazorShared.Request;
#nullable disable
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Net.NetworkInformation;
using FoodHub.BlazorShared.Dto;
using FoodHub.Blazor.Client.Services;
using FoodHub.Blazor.Client.Config;
namespace FoodHub.Blazor.Client.Authentication
{
    public class RefreshAccessTokenService 
    {
        private readonly TokenStorage _tokenStorage;

        private readonly HttpClient _httpClient;

        private readonly string _apiUri;

        private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;
        
        public RefreshAccessTokenService(TokenStorage tokenStorage,HttpClient httpClient,IOptions<BaseUrlConfiguration> options,CustomAuthenticationStateProvider customAuthenticationStateProvider) {

            _tokenStorage = tokenStorage;

            _httpClient = httpClient;

            _apiUri = options?.Value.ApiBase;

            _customAuthenticationStateProvider = customAuthenticationStateProvider;
        }


        public async Task<bool> RefreshAccessTokenAsync() {

            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            if (string.IsNullOrEmpty(refreshToken)) {

                return false;

            }

            var tokenStorageMode = await _tokenStorage.GetStorageModeAsync();   

            var request = new RefreshTokenReuqest
            {
                RefreshToken = refreshToken ,

                TokenStorageMode = tokenStorageMode

            };

            var content = ToJson(request);

            var response = await _httpClient.PostAsync($"{_apiUri}auth/refresh", content);

            if (!response.IsSuccessStatusCode) {
                return false;
            }

            var tokenResult = await FromHttpMessage<TokenResultDto>(response);

            await _tokenStorage.SaveTokenAsync(tokenResult);

            _customAuthenticationStateProvider.NotifyUserAuthentication(tokenResult.AccessToken);

            return true;
        }



        private StringContent ToJson(object obj) {

            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
        
        }

        private async Task<T> FromHttpMessage<T>(HttpResponseMessage reponse) {

            return JsonSerializer.Deserialize<T>(await reponse.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        
        }


    }
}
