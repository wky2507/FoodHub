using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared.Response
{
    public class LogoutResponse : BaseResponse
    {
        public LogoutResponse(bool success) {

            Success = success;

        }

        public LogoutResponse() { }
        public bool Success { get; set; }

    }
}
