using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using FoodHub.Blazor.Client.Security;
using FoodHub.Blazor.Client.Services;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Options;
using FoodHub.BlazorShared.Dto;
namespace FoodHub.Blazor.Client.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly TokenStorage _tokenStorage;

        private readonly HttpClient _httpClient;

        private readonly string _apiUrl;

        //注入HttpService会导致形成懒加载，递归死循环
        //private readonly HttpService _httpService;

        public CustomAuthenticationStateProvider(TokenStorage tokenStorage,HttpClient httpClient,IOptions<BaseUrlConfiguration> options)
        {
            _tokenStorage = tokenStorage;

            _httpClient = httpClient;

            _apiUrl = options.Value!.ApiBase; 
        }
   
        private bool IsTokenExpired(string token) {

            if (string.IsNullOrEmpty(token)) {
                return true;
            }
            try
            {
                var handler = new JwtSecurityTokenHandler();

                var jwtToken = handler.ReadJwtToken(token);

                return jwtToken.ValidTo < DateTime.UtcNow;
            }

            catch {

                return true;
            
            }    
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var accessToken = await _tokenStorage.GetAccessTokenAsync();

            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            //拿不出来，说明没有勾选rememberMe，直接算不通过认证,只存了session
            if (string.IsNullOrWhiteSpace(accessToken) || string.IsNullOrWhiteSpace(refreshToken))
            {

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            }

            var accessTokenExpire = IsTokenExpired(accessToken);

            var freshTokenExpire = await _tokenStorage.GetRefreshTokkenExpireAtAsync() < DateTime.UtcNow;

           
            //Local
            if (accessTokenExpire && freshTokenExpire) {

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            }
            //Local
            else if(accessTokenExpire && !freshTokenExpire) {

                var content = Tojson(refreshToken);

                //尝试拿新的请求,如果被401，会被拦截跳转
                var refreshTokenRequest = new RefreshTokenReuqest {
                    RefreshToken = refreshToken,
                    TokenStorageMode = TokenStorageMode.Local
                };

                // var res = await _httpService.HttpPost<TokenResult>("auth/refresh", refreshTokenRequest);
                var res = await _httpClient.PostAsync($"{_apiUrl}auth/refresh", content);

                //冗余设计，安全保底
                if (!res.IsSuccessStatusCode)
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                var result = await FromHttpResponseMessage<TokenResultDto>(res);

                if (result != null)
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(result.AccessToken), "jwt")));
                }
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            //这里不负责验证拦截，不做校验，都放行
            var identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(accessToken), "jwt");

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string token) 
        {
            ClaimsPrincipal authenticatedUser;

            if (string.IsNullOrWhiteSpace(token))
            {

                authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity());

            }

            else {

                var identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwt");
                
                authenticatedUser = new ClaimsPrincipal(identity);
            
            }

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            NotifyAuthenticationStateChanged(authState);
        
        }

        public async void NotifyLogout() {
            //前端清除Token
            await _tokenStorage.RemoveTokenAsync(true);
            
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

            var authState = Task.FromResult(new AuthenticationState(anonymous));

            NotifyAuthenticationStateChanged(authState);

        }

        private StringContent Tojson(object obj) {

            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
        
        }

        private async Task<T?> FromHttpResponseMessage<T>(HttpResponseMessage result) where T : class
        {

            if (result.IsSuccessStatusCode || result != null) {
                
                var json = await result.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(json)) {

                    return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                }

            }

            return default;
        
        }

    }
}
