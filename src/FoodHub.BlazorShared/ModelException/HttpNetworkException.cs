using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared.ModelException
{
    public class HttpNetworkException :Exception
    {
        public HttpNetworkException(string mes,Exception? ex) : base(mes,ex){ }

    }
}
