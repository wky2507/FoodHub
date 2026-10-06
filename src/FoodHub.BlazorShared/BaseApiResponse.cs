using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared
{
    public class BaseApiResponse<T> : BaseResponse where T : class
    {
        public int Code { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }
    }
}
