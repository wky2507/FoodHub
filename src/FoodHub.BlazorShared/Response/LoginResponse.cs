using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodHub.BlazorShared.Dto;
#nullable disable
namespace FoodHub.BlazorShared.Response
{
   public class LoginResponse : BaseResponse
   {

        public TokenResultDto TokenResult { get; set; } = new();

        public LoginResponse() { }

        public LoginResponse(TokenResultDto tokenResultDto):this() {

   
            TokenResult = tokenResultDto;

        }

    }
}
