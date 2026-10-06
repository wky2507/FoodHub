#nullable disable
using FoodHub.BlazorShared.Response;
using FoodHub.Blazor.Client.Services;
using FoodHub.Blazor.Client.Authentication;
using Microsoft.Extensions.Options;
using FoodHub.BlazorShared.Dto;

namespace FoodHub.Blazor.Client.ApiService
{
    public class AuthService
    {

        private readonly HttpService _httpService;

        private readonly ILogger<AuthService> _logger;

        private readonly CustomAuthenticationStateProvider _customAuthenticationStateProvider;

        private readonly TokenStorage _tokenStorage;

        public AuthService(HttpService httpService, ILogger<AuthService> logger, CustomAuthenticationStateProvider customAuthenticationStateProvider,TokenStorage tokenStorage, IOptions<BaseUrlConfiguration> options)
        {
            _httpService = httpService;

            _logger = logger;

            _customAuthenticationStateProvider = customAuthenticationStateProvider;

            _tokenStorage = tokenStorage;
        }

        public async Task<HttpCallResultDto<LoginResponse>> LoginAsync(LoginRequest loginRequest)
        {
                if (loginRequest == null)
                {
                    string msg = "登录请求LoginRequest不能为null";

                    _logger.LogError(msg);

                    throw new ArgumentNullException(nameof(loginRequest), msg);
                }

           
                if (loginRequest.RememberMe)
                {
                    loginRequest.TokenStorageMode = TokenStorageMode.Local;

                    await _tokenStorage.SetStorageModeAsync(TokenStorageMode.Local);
                    
                }
                else {

                    loginRequest.TokenStorageMode = TokenStorageMode.Session;

                    await _tokenStorage.SetStorageModeAsync(TokenStorageMode.Session);
                
                }

                 var res = await _httpService.HttpPost<LoginResponse>("auth/login", loginRequest);

                 if (res.IsSuccess) {

                    var tokenResult = res.Data?.TokenResult;

                    await _tokenStorage.SaveTokenAsync(tokenResult);

                    _customAuthenticationStateProvider.NotifyUserAuthentication(tokenResult.AccessToken);
                }   


                return res;
                
                

        }

        public async Task LogoutAsync() {

            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            var accessToken = await _tokenStorage.GetAccessTokenAsync();

            var logoutRequest = new LogoutRequest
            {
                AccessToken = accessToken,

                RefreshToken = refreshToken
            };

           var res = await _httpService.HttpPost<LogoutResponse>("auth/logout",logoutRequest);


            if (res != null && res.Data.Success)
            {
                _customAuthenticationStateProvider.NotifyLogout();
            }
           
            //不成功的返回，有人帮你处理了，暂时不用写
        }

    }
}
