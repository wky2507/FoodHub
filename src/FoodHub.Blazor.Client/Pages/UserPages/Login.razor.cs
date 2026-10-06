using FoodHub.Blazor.Client.Helper;
using FoodHub.Blazor.Client.Services;
using Microsoft.AspNetCore.Components;

#  nullable disable
namespace FoodHub.Blazor.Client.Pages.UserPages
{
     public partial class Login : BlazorComponent
    {

        private LoginRequest LoginModel = new LoginRequest();

        private string ErrorMessage { get; set; } = string.Empty;

        private bool IsSubmitting { get; set; }
        [Inject]
        private  AuthService _authService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private ILogger<Login> _logger { get; set; }

        [Inject]
        private TokenStorage _tokenStorage { get; set; }

        [Inject]
        private  ToastService _toastService { get; set; }

        private async Task Login_Click() {

            IsSubmitting = true;

            ErrorMessage = null;

            Console.WriteLine("login 执行打印");
            try
            {
                if (LoginModel.UserName.Length == 0 || LoginModel.Password.Length == 0)
                {
                    _logger.LogError("用户未输入账号或者密码");
                    return;
                }

                var res = await _authService.LoginAsync(LoginModel);

                //登录页被干掉回不去
                if (res.IsSuccess)
                {
                    _navigationManager.NavigateTo("/", replace: true);
                }
                else
                {
                    ErrorMessage = res.Message;
                }
            }
            catch (ArgumentNullException ex)
            {
                _toastService.ShowError(ex.Message, "参数异常");

                _logger.LogError(ex.Message);
            }
            catch (HttpNetworkException ex)
            {

                _toastService.ShowError(ex.Message, "网络异常");

                _logger.LogError(ex.Message);
            }
            catch (Exception e) {

                _logger.LogError(e.Message);

            }
            finally
            {

                IsSubmitting = false;

            }
     }
       
    }
}
