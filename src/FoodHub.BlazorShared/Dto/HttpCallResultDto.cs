using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared.Dto
{
    public class HttpCallResultDto<T> where T : new()
    {

        public T? Data { get; set; } = new();

        public bool IsSuccess { get; set; } = true;

        //属于客户端故障ErrorMessage、OriginalException才会填写
        public string Message { get; set; } = string.Empty;

        public HttpStatusCode StatusCode { get; set; }

        public Exception? OriginalException { get; set; }

        public static explicit operator HttpCallResultDto<T>(Task v)
        {
            throw new NotImplementedException();
        }
    }
}
