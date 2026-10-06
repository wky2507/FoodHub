#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared.Request
{
    public class RegisterRequest : BaseRequest
    {
        public RegisterRequest() { }

        public string UserName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;


        public string Email { get; set; } = string.Empty;


        public string Password { get; set; } = string.Empty;
    }
}
