using Blazored.LocalStorage;
using Blazored.SessionStorage;
using FoodHub.BlazorShared.Dto;
namespace FoodHub.Blazor.Client.Services
{
    public class TokenStorage
    {
        private readonly ILocalStorageService _localStorageService;

        private readonly ISessionStorageService _sessionStorageService;

        private const string AccessTokenKey = "AccessToken";

        private const string RefreshTokenKey = "RefreshToken";

        private const string StorageModeKey = "TokenStorageMode";

        private const string AccessTokenExpireKey = "AccessTokenExpireAt";

        private const string RefreshTokenExpireKey = "RefreshTokenExpireAt";


        public TokenStorage(ILocalStorageService localStorageService, ISessionStorageService sessionStorageService) {
            
            _localStorageService = localStorageService;


            _sessionStorageService = sessionStorageService;

        }

   
        //登录接口用一次
        public async Task SetStorageModeAsync(TokenStorageMode tokenStorageMode) {

            if (!Enum.IsDefined(typeof(TokenStorageMode), tokenStorageMode)) {

                throw new ArgumentOutOfRangeException(nameof(tokenStorageMode), $"传入的参数{tokenStorageMode}无效非法!");

            }

            await _localStorageService.SetItemAsync<string>(StorageModeKey, tokenStorageMode.ToString());

        }

        
        public async Task<TokenStorageMode> GetStorageModeAsync(){

            var storageMode = await _localStorageService.GetItemAsync<string>(StorageModeKey);

            if (Enum.TryParse<TokenStorageMode>(storageMode, out TokenStorageMode tokenStorageMode)) {

                return tokenStorageMode;
            
            }

            return TokenStorageMode.Session;
            
        }




        //保存Token到storage
        public async Task SaveTokenAsync(TokenResultDto tokenResult)
        {
            await RemoveTokenAsync(false);

            var tokenStorageMode = await GetStorageModeAsync();


            if (tokenStorageMode is TokenStorageMode.Local)
            {

                await _localStorageService.SetItemAsync(AccessTokenKey, tokenResult.AccessToken);

                await _localStorageService.SetItemAsync(RefreshTokenKey, tokenResult.RefreshToken);

                await _localStorageService.SetItemAsync(AccessTokenExpireKey, tokenResult.AccessTokenExpire);

                await _localStorageService.SetItemAsync(RefreshTokenExpireKey, tokenResult.RefreshTokenExpire);

            }
            else {

                await _sessionStorageService.SetItemAsync(AccessTokenKey, tokenResult.AccessToken);

                await _sessionStorageService.SetItemAsync(RefreshTokenKey, tokenResult.RefreshToken);

                await _sessionStorageService.SetItemAsync(AccessTokenExpireKey, tokenResult.AccessTokenExpire);

                await _sessionStorageService.SetItemAsync(RefreshTokenExpireKey, tokenResult.RefreshTokenExpire);

            }

        }

        public async Task<string?> GetAccessTokenAsync() 
        {
            var mode = await GetStorageModeAsync();

            if (mode == TokenStorageMode.Local) {

                return await _localStorageService.GetItemAsync<string>(AccessTokenKey);

            }

            return await _sessionStorageService.GetItemAsync<string>(AccessTokenKey);
        
        }

        public async Task<string?> GetRefreshTokenAsync() {

            var mode = await GetStorageModeAsync();

            if (mode == TokenStorageMode.Local) {

                return await _localStorageService.GetItemAsync<string>(RefreshTokenKey);

            }
            return await _sessionStorageService.GetItemAsync<string>(RefreshTokenKey);
        }

        //一次性刷新，清空旧的
        public async Task RemoveTokenAsync(bool removeMode) {

            await _sessionStorageService.RemoveItemAsync(AccessTokenKey);

            await _sessionStorageService.RemoveItemAsync(RefreshTokenKey);

            await _localStorageService.RemoveItemAsync(AccessTokenKey);

            await _localStorageService.RemoveItemAsync(RefreshTokenKey);

            if (removeMode) {

                await _localStorageService.RemoveItemAsync(StorageModeKey);
            
            }

        }

        public async Task<DateTime> GetRefreshTokkenExpireAtAsync() {

            var expireTime = await _sessionStorageService.GetItemAsync<string>(RefreshTokenExpireKey);

            if (string.IsNullOrWhiteSpace(expireTime)) {
                expireTime = await _localStorageService.GetItemAsync<string>(RefreshTokenExpireKey);
            }

            if (!string.IsNullOrWhiteSpace(expireTime) && DateTime.TryParse(expireTime, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out var expireAt))
            {
                return expireAt;
            
            }

            return DateTime.MinValue;
        }

    }
}
