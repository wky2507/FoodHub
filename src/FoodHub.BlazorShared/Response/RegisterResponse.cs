using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared.Response
{
    public class RegisterResponse : BaseResponse
    {
        public RegisterResponse() { }

        public RegisterResponse(Guid guid) :base(guid){ }

        public bool Result { get; set; }

        public string Message { get; set; } = string.Empty;

        public string Id { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;
    }

}
