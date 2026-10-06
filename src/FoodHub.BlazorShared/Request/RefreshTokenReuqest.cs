
using FoodHub.Domain.Share.Constants;

namespace FoodHub.BlazorShared.Request
{
    public class RefreshTokenReuqest : BaseRequest
    {

        public string RefreshToken { get; init; } = string.Empty;

        public TokenStorageMode TokenStorageMode { get; init; } 
    }
}
