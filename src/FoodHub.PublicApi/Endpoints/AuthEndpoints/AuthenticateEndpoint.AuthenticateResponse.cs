#nullable disable
using FoodHub.BlazorShared.Dto;
using FoodHub.BlazorShared;
namespace FoodHub.PublicApi.AuthEndpoints
{
    public class AuthenticateResponse : BaseResponse
    {

       public AuthenticateResponse(Guid guid) {

            base.guid = guid;
        
        }

        public AuthenticateResponse() { }

        public TokenResultDto TokenResult { get; set; } = new();

    }
}
