
using FoodHub.Domain.Share.Constants;
using System.ComponentModel.DataAnnotations;
#nullable disable
namespace FoodHub.BlazorShared.Request
{
    public class LoginRequest : BaseRequest
    {
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "记住我")]
        public bool RememberMe { get; set; } 

        public TokenStorageMode TokenStorageMode { get; set; }

       public LoginRequest() { }

      
    }
}
