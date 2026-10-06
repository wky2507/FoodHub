using FoodHub.Blazor.Client.Services;
using FoodHub.BlazorShared.Request;
using FoodHub.BlazorShared.Response;
using System.Threading.Tasks;
namespace FoodHub.Blazor.Client.ApiService
{
    public class RegisterService
    {
        private readonly HttpService _httpService;

        public RegisterService(HttpService httpService) {

            _httpService = httpService;
        
        }


        public async Task<RegisterResponse?> Register(RegisterRequest registerRequest)
        {
           
             if (registerRequest == null || registerRequest.UserName == null || registerRequest.Phone == null || registerRequest.Email == null || registerRequest.Password == null)
             {
                string mes = "参数为空报错";

               throw new ArgumentNullException(mes);
             }
          
             var res = await  _httpService.HttpPost<RegisterResponse>("auth/register", registerRequest);

             return res.Data;

        }

      
    }
}
