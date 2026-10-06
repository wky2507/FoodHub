#nullable disable
using FoodHub.BlazorShared;
namespace FoodHub.PublicApi.AuthEndpoints
{
    public class AuthenticateRequest : BaseRequest
    {
  
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public string RefreshToken { get; set; }
        
        public TokenStorageMode TokenStorageMode { get; set; }

    }
}
